using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Core
{
    // delegate for handing response information in events
    public delegate void ResponseHandler(Response response);

    /// <summary>
    /// A class that represents a client to server connection. 
    /// Sends requests to the server and returns responses 
    /// to the client and takes care of handling events from view models.
    /// </summary>
    public class NetworkConnection : Notifier
    {
        #region Public Events

        // wakes up if server granted the request and sent the data after login
        public event ResponseHandler SignInDone;

        // wakes up if sign in failed
        public event ResponseHandler SignInFail;

        #endregion

        #region Public Properties

        // try if are we now waiting for an answer to some of our requests
        public bool WaitingResponse { get; set; }

        // current tcp connection 
        public TcpClient TcpClient { get; set; }

        // current user if the client is authorized 
        public User User { get; set; }

        // true if authorized
        public bool Authorized
        {
            get => User != null;
            set => Authorized = value;
        }

        // all requests from view models go to the 
        // end of this queue for further processing
        public Queue<Request> RequestQueue { get; set; }

        #endregion

        #region Private Properties

        // stream between Client and server
        NetworkStream NetworkStream { get; set; }

        #endregion

        #region Constructor

        public NetworkConnection()
        {
            // start action in thread pool
            Task.Run(() =>
            {
                // this method should work throughout the 
                // entire connection to the server
                _ = ProcessAsync()
                        // run in parallel in a different synchronization context
                        .ConfigureAwait(continueOnCapturedContext: false);
            });
        }

        #endregion

        #region Public Methods

        // processing this newtwork connection
        public async Task ProcessAsync()
        {
            // create new request queue
            RequestQueue = new Queue<Request>();

            // creating current tcp client
            TcpClient = new TcpClient();

            // connecting to server async
            await TcpClient.ConnectAsync("127.0.0.1", 1300);

            // get newtwork stream of communication with the server
            NetworkStream = TcpClient.GetStream();

            #region Subscribe On View Model Events

            // adding new request in queue
            IoC.Get<SignInViewModel>().SignInFromViewModel += (
                ss, request) => RequestQueue.Enqueue(request);

            #endregion

            // main loop of process method, represents connection to server
            while (true)
            {
                // if we don't wait a response from server and have request
                if (!WaitingResponse
                    && RequestQueue.Count > 0)
                {
                    WaitingResponse = true;

                    // remuve and return object at the beginning of the Request Queue
                    var firstQueueRequest = RequestQueue.Dequeue();

                    // send server request
                    await WriteToStreamAsyncAndSerialize(firstQueueRequest);

                    // actions after sending a request
                    switch (firstQueueRequest.UserRequestType)
                    {
                        case UserRequest.SendMessage:
                            {
                                // we dont want wait response server
                                WaitingResponse = false;
                                break;
                            }
                            
                        //case UserRequest.SignIn:
                        //    break;
                        //case UserRequest.SignUp:
                        //    break;
                        //case UserRequest.GiveNewMessages:
                        //    break;
                        //case UserRequest.CreateNewChat:
                        //    break;
                        default:
                            break;
                    }
                }

                // if we send to server request and waiting response
                if (WaitingResponse)
                {
                    // wait server response
                    var response = await ReadFromStreamAsyncAndDeserialize();

                    // todo: response reaction action
                    CheckResponseTypeAndWakeUpEvents(response);

                    // we dont wait response and ready to send new requests
                    WaitingResponse = false;
                }
            }
        }

        // create a request to check for new messages every 5 seconds
        public async Task CreateRequestCheckNewMessages()
        {
            while (true)
            {
                // wait 5 seconds
                await Task.Delay(1000);

                // tell the server to check if there are new messages
                RequestQueue.Enqueue(
                    new Request
                    {
                        UserRequestType = UserRequest.GiveNewMessages
                    });
            }
        }

        // subscribe to the user to send new messages in view model
        public void SubscribeOnNewMessagesInChats() 
        {
            // go over all user chats
            foreach (var chat in IoC.Get<ChatListViewModel>().Items)
            {
                // subscribe to the event that occurs when we send a new message
                chat.CurrentChatMessageList.NewChatMessage += (message) =>
                {
                    // a function call that collects a request for the server
                    SendMyNewChatMessageToServer(chat, message);
                };
            }
        }

        // if we have new author message, we want send 
        // him to server for update chat in db
        public void SendMyNewChatMessageToServer(
            ChatListItemViewModel chatFrom, ChatMessageListItemViewModel newMessages)
        {
            // create message entity
            var messageEntity =
                new Message 
                { 
                    ChatId = chatFrom.CurrentChat.Id,
                    AuthorUser = this.User,
                    CreatedAt = newMessages.MessageSentTime,
                    Text = newMessages.Message
                };

            // create and queue for execution
            RequestQueue.Enqueue(
                new Request
                {
                    Message = messageEntity,
                    UserRequestType = UserRequest.SendMessage,
                    UserInitiator = this.User,

                    // we do not assign the object directly so as not to 
                    // transmit all messages to the server, this is unnecessary here
                    TargetChat = 
                        new Chat 
                        { 
                            Id = chatFrom.CurrentChat.Id,
                            MemberUser = chatFrom.CurrentChat.MemberUser,
                            OwnerUser = chatFrom.CurrentChat.OwnerUser,
                            CreatedAt = chatFrom.CurrentChat.CreatedAt,
                            IsChannel = chatFrom.CurrentChat.IsChannel
                        }
                });
        }

        // reaction to server response according to response type
        public void CheckResponseTypeAndWakeUpEvents(Response response)
        {
            switch (response.ServerResponseType)
            {
                case ServerResponse.SignInFailed:
                    {
                        // wakes event
                        SignInFail(response);

                        break;
                    }

                case ServerResponse.SignUpFailed:
                    break;

                case ServerResponse.SendAllInformation:
                    {
                        // create an instance of this user
                        this.User = new User();

                        // set data about this user received from the server
                        this.User = response.UserInfo;

                        // collect the data received from the server 
                        // in a relevant form and send it to the view model
                        CreateChatsItemsAndSendToViewModel(
                            response.Chats, this.User.Id, this.User.Nick)
                            .ConfigureAwait(false);

                        // subscribe to the user to send new messages
                        SubscribeOnNewMessagesInChats();

                        // logically, we do not have a server response 
                        // about a successful login, it immediately sends data
                        SignInDone(response);

                        // run a task that will create and add a 
                        // request to check for new messages in the background every 5 seconds
                        _ = CreateRequestCheckNewMessages()
                            // run in parallel in a different synchronization context
                            .ConfigureAwait(false);

                        break;
                    }

                case ServerResponse.SendNewMessages:
                    {
                        // means there are no new chats for us
                        if (response.Chats == null)
                        {
                            break;
                        }

                        // check chats and messages and if there 
                        // are new ones send them to the view model
                        AddNewMessagesToChatViewModel(
                            response.Chats, this.User.Id, this.User.Nick);

                        // subscribe to the user to send new messages
                        SubscribeOnNewMessagesInChats();       

                        break;
                    } 

                case ServerResponse.Error:
                    break;

                default:
                    break;
            }
        }

        // send chats and messages that came 
        // from the server to the view model
        public async Task CreateChatsItemsAndSendToViewModel(
            IList<Chat> chats, long userId, string userNick)
        {
            // create a list for local work in a method
            var items = new List<ChatListItemViewModel>();

            // create dialogs according to the content of chats
            foreach (var chat in chats)
            {
                // create chat list item view model from chat
                items.Add(ChatToItem(chat, userId, userNick));
            }

            // get the items from chat list view model and set updated data
            IoC.Get<ChatListViewModel>().Items = items;

            // we added chats, you need to subscribe to them
            //await IoC.Get<ChatListViewModel>().SubscribeOnChatSelectedInChats();
        }

        // if we want to check if there are new posts 
        // and add them to the view model
        public void AddNewMessagesToChatViewModel(
            IList<Chat> chatsFromServer, long userId, string userNick)
        {
            if (chatsFromServer == null)
            {
                return;
            }

            var chatListViewModel = (ChatListViewModel)IoC.Get<ChatListViewModel>().Clone();

            // take the current list of messages
            var items = chatListViewModel.Items;

            //// take the current list of messages
            //var items = IoC.Get<ChatListViewModel>().Items;

            // empty list for check difference
            var newItems = new List<ChatListItemViewModel>();

            // we go through all the chats that came from the server
            foreach (var chatFromServer in chatsFromServer)
            {
                // iterating over all current chats
                foreach (var chatListItemViewModel in items)
                {
                    // if chat id from server not equal chat id from client 
                    if (chatFromServer.Id != chatListItemViewModel.CurrentChat.Id)
                    {
                        continue;
                    }

                    // find excepted messages in the current chat messages
                    var newMessages =
                        chatFromServer.Messages
                            .Except(chatListItemViewModel.CurrentChat.Messages, new MessageIdComparer())
                            .ToList();

                    // if we have all messages
                    if (newMessages.Count == 0)
                    {
                        // just copy previous item
                        newItems.Add(chatListItemViewModel);

                        continue;
                    }

                    // adding new messages
                    foreach (var newMessage in newMessages)
                    {
                        // add new messages to current chat
                        chatListItemViewModel.CurrentChat.Messages.Add(newMessage);    
                    }


                    // add this new chat list item view model
                    newItems.Add(

                        // we changed the messages in the chat and 
                        // now we want to display beautifully
                        ChatToItem(   
                            chat: chatListItemViewModel.CurrentChat,
                            userId,
                            userNick,
                            isChatSelected: chatListItemViewModel.IsSelected
                            //oldChatListItemViewModel: chatListItemViewModel
                            )
                        );
                }
            }

            // if we have some changed in items
            if (items.Equals(newItems))
            {
                return;
            }
            // for debug
            //var currentItems = IoC.Get<ChatListViewModel>().Items;

            // set chats and new message inside
            IoC.Get<ChatListViewModel>().Items = newItems;

            // we added chats, you need to subscribe to them
            //IoC.Get<ChatListViewModel>().SubscribeOnChatSelectedInChats();

            // check if there is an open chat now
            var openedChatLocalVariable = IoC.Get<ChatListViewModel>().Items
                .Where(i => i.IsSelected == true)
                .FirstOrDefault(); 

            // if no chat is open, you do not need to refresh the chat page
            if (openedChatLocalVariable == null)
            {
                return;
            }

            // find current opened chat and
            // restart message list page to refresh the data on the page
            IoC.Get<ChatListViewModel>().Items
                .Where(i => i.IsSelected == true)
                .FirstOrDefault()
                .OpenMessage();
        }

        // create chat list item view model
        public ChatListItemViewModel ChatToItem(
            Chat chat, long userId, string userNick, 
            bool isChatSelected = false, ChatListItemViewModel oldChatListItemViewModel = null)
        {
            // objects that form a list of bubble messages for this chat
            var bubbleMessagesList = new ObservableCollection<ChatMessageListItemViewModel>();


            if (chat.Messages != null)
            {
                // collect a collection of messages for this dialog
                foreach (var chatMessage in chat.Messages)
                {
                    // if the author of the message is me
                    if (chatMessage.AuthorUser.Id == userId)
                    {
                        // add to the end of dialog new message bubble
                        bubbleMessagesList.Add(
                        new ChatMessageListItemViewModel
                        {
                            Message = chatMessage.Text,

                            // initials from first letters of ...
                            ProfileInitials =
                                // ... first name and 
                                chatMessage.AuthorUser.FirstName.First().ToString()
                                // ... of last name
                                + chatMessage.AuthorUser.LastName.First().ToString(),

                            ProfilePictureRGB = "63c439",
                            SenderName = chatMessage.AuthorUser.Nick,
                            MessageSentTime = chatMessage.CreatedAt,
                            IsSelected = false,
                            AnchorVisibility = true,
                            ShowProfilePicture = false,
                            ImAuthor = true
                        });
                    }

                    // if the author of the message is not me
                    else
                    {
                        // add to the end of dialog new message bubble
                        bubbleMessagesList.Add(
                        new ChatMessageListItemViewModel
                        {
                            Message = chatMessage.Text,

                            // initials from first letters of ...
                            ProfileInitials =
                                // ... first name and 
                                chatMessage.AuthorUser.FirstName.First().ToString().ToUpper()
                                // ... of last name
                                + chatMessage.AuthorUser.LastName.First().ToString().ToUpper(),

                            ProfilePictureRGB = "c46339",
                            SenderName = chatMessage.AuthorUser.Nick,
                            MessageSentTime = chatMessage.CreatedAt,
                            IsSelected = false,
                            AnchorVisibility = true,
                            ShowProfilePicture = false,
                            ImAuthor = false
                        });
                    }
                }
            }

            var sortetBubbleMessagesList =
                new ObservableCollection<ChatMessageListItemViewModel>(
                    bubbleMessagesList.OrderBy(c => c.MessageSentTime));
            

            // if the user wrote something at the moment when 
            // we updated his chat, then this must be saved
            string pendingTextOfnewChatItem = 
                oldChatListItemViewModel == null ? 
                null : oldChatListItemViewModel.CurrentChatMessageList.PendingMessageText;

            // add a new chat with messages to Chat List Item
            var newChatItem = new ChatListItemViewModel
            {
                CurrentChat = chat,

                // if current chat is active chat
                IsSelected = isChatSelected,

                // put the name of my interlocutor in the title
                Name =
                    chat.OwnerUser.Id == userId ?
                    chat.MemberUser.Nick : chat.OwnerUser.Nick,

                // put the first letter of the nickname 
                // of my interlocutor in the initials
                ProfileInitials =
                    chat.OwnerUser.Id == userId ?
                    chat.MemberUser.Nick.First().ToString().ToUpper()
                    : chat.OwnerUser.Nick.First().ToString().ToUpper(),

                // set background color of initials
                ProfilePictureRGB = "c46339",

                // put the last message from this chat
                Message = chat.Messages == null ? "" : chat.Messages.Last().Text,

                // create bubbles messages
                CurrentChatMessageList = new ChatMessageListViewModel
                {
                    // if the user wrote something at the moment when we 
                    // updated his chat, then this must be saved
                    PendingMessageText = pendingTextOfnewChatItem,

                    // set initials of this user for new messages
                    AuthorProfileInitials =
                    userNick.First().ToString().ToUpper(),

                    // set author name for new messages sended from this client
                    AuthorName = userNick,

                    // put the the nickname 
                    // of my interlocutor in the title
                    DisplayTitle =
                    chat.OwnerUser.Id == userId ?
                    chat.MemberUser.Nick : chat.OwnerUser.Nick,

                    // add messages in chat
                    Items = sortetBubbleMessagesList == null ? 
                        new ObservableCollection<ChatMessageListItemViewModel>() : sortetBubbleMessagesList
                }
            };

            return newChatItem;
        }

        #endregion

        #region Stream Heplers

        /// <summary>
        /// Deserializes the request received from the user 
        /// and provides the <see cref="Request"/> object.
        /// </summary>
        /// <returns></returns>
        private async Task<Response> ReadFromStreamAsyncAndDeserialize()
        {
            // create empty buffer
            byte[] buffer = new byte[4096];

            var receiveStringBuilder = new StringBuilder();

            // current position in the NetworkStream
            int streamPosition = 0;

            do
            {
                // async read from stream
                streamPosition = await NetworkStream.ReadAsync(buffer, 0, buffer.Length)
                        // run in parallel in a different synchronization context
                        .ConfigureAwait(continueOnCapturedContext: false);

                // encoding one character at a time and appending 
                // it to the incoming receive string
                receiveStringBuilder.Append(
                    Encoding.UTF8.GetString(buffer, 0, streamPosition));

            } while (NetworkStream.DataAvailable);

            // convert bytes to string
            var jsonString = receiveStringBuilder.ToString();

            // deserialize json string to request
            var response = JsonConvert.DeserializeObject<Response>(jsonString);

            return response;
        }

        /// <summary>
        /// Serializes and sends <see cref="Request"/> 
        /// to the <see cref="NetworkStream"/> to server
        /// </summary>
        /// <param name="serverResponse">Object of server Response</param>
        /// <returns></returns>
        public async Task<bool> WriteToStreamAsyncAndSerialize(Request myNewRequest)
        {
            // boolean result of this task
            var tcs = new TaskCompletionSource<bool>();
            
            /*string jsonString = $"Hello! Password:{myNewRequest.UserInitiator.Password}, Login:{myNewRequest.UserInitiator.Email}";*/

            try
            {
                // serialize object to json string
                string jsonString = JsonConvert.SerializeObject(myNewRequest);

                // encoding string to bytes
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

                // async write bytes in network stream
                await NetworkStream.WriteAsync(jsonBytes, 0, jsonBytes.Length)
                        // run in parallel in a different synchronization context
                        .ConfigureAwait(continueOnCapturedContext: false);

                tcs.SetResult(true);
            }
            catch
            {
                tcs.SetResult(false);
            }

            // we cannot send other requests until the server responds to our request
            WaitingResponse = true;

            return await tcs.Task;
        }

        #endregion
    } 
}
