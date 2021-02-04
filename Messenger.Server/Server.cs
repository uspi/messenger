using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using Messenger.Core;
using System.Threading;

namespace Messenger.Server
{
    /// <summary>
    /// Controls the number of <see cref="ServerUsers"/> and transfers <see cref="UserRequest"/> 
    /// from <see cref="UserEnvironment"/> to <see cref="Messenger.Server.DataBaseContext"/>
    /// </summary>
    public class Server : IDisposable
    {
        #region Public Properties

        // server tcp listener
        public TcpListener TcpListener { get; set; }

        // current data base context
        public DataBaseContext DataBaseContext { get; set; }

        // server ip addres for input connections
        public IPAddress IPAddress { get; set; }

        // server port for input connections
        public int Port { get; set; }

        // users connected to the server
        public List<UserEnvironment> ServerUsers { get; set; } = new List<UserEnvironment>();

        #endregion

        #region Constructor

        public Server(IPAddress ipAddress, int port, DataBaseContext context)
        {
            // set server ip address
            IPAddress = ipAddress;

            // set server port
            Port = port;

            // set server database context
            DataBaseContext = context;
        }

        #endregion 

        #region User Environment Events Reaction

        // when the client asks if there are new messages 
        // in the database that he does not have on the client
        private void UserEnvironment_CheckNewMessageForMe(
            UserEnvironment sender, Request request)
        {
            // chats that correspond to chats on the client
            var currentUserChats = new List<Chat>(sender.SynchronizedChats.Clone()); 

            // empty list which will contain the output collection
            var updatedChats = new List<Chat>();

            // find all user chats
            var allTargeUserChats = DataBaseContext.Chats
                .Where(c => c.MemberUser.Id == sender.User.Id
                          || c.OwnerUser.Id == sender.User.Id)
                .ToList();

            // find all needed data for all of user chats 
            foreach (var item in allTargeUserChats)
            {
                // find all messages for current chat in db
                var messagesForCurrentChat = DataBaseContext.Messages
                                    .Where(m => m.ChatId == item.Id)
                                    .ToList();

                // find all members of current chat
                // so far, it is assumed that a chat 
                // can only have one member
                var membersOfCurrentChat = DataBaseContext.Chats
                                .Where(ch => ch.Id == item.Id)
                                .Select(r => r.MemberUser)
                                .ToList();

                // find all members of current chat
                // so far, it is assumed that a chat 
                // can only have one owner
                var ownersOfCurrentChat = DataBaseContext.Chats
                                .Where(ch => ch.Id == item.Id)
                                .Select(r => r.OwnerUser)
                                .ToList();
            }

            //// what chats are on the server but they are not on the client
            //var newChats = allTargeUserChats
            //    .Except(currentUserChats)
            //    .ToList();


            // check for missing messages in each of the chats
            foreach (var chatFromClient in currentUserChats) 
            {
                foreach (var chatFromServer in allTargeUserChats)
                {
                    // if chat id from server not equal chat id from client
                    if (chatFromServer.Id != chatFromClient.Id)
                    {
                        continue;
                    }

                    // if messages not have messages
                    if (chatFromServer.Messages == null)
                    {
                        // just copy this synchronized current user chat
                        updatedChats.Add(chatFromClient);
                        continue;
                    }

                    // find excepted messages in the current chat messages
                    var newMessagesInChat =
                        chatFromServer.Messages
                            .Except(chatFromClient.Messages, new MessageIdComparer())
                            .ToList();


                    // if we synchronized all messages
                    if (newMessagesInChat.Count == 0)
                    {
                        // just copy this synchronized current user chat
                        updatedChats.Add(chatFromClient);

                        continue;
                    }

                    // adding new messages in chat
                    foreach (var newMessage in newMessagesInChat)
                    {
                        chatFromClient.Messages.Add(newMessage);
                    }

                    // if we added new messages in chat then add 
                    // this chat to updated chats
                    updatedChats.Add(chatFromClient);
                }  
            }

            //// if there are chats that are on the server 
            //// but they are not on the client
            //if (newChats.Count > 0)
            //{
            //    // adding new chats
            //    foreach (var newChat in newChats)
            //    {
            //        // add this new chat 
            //        updatedChats.Add(newChat);
            //    }
            //}

            // if we have some changed in chats
            if (!sender.SynchronizedChats.SequenceEqual(updatedChats, new ChatCountComparer()))
            {
                //send chats with new messages to client
               sender.ResponseQueue.Enqueue(
                    new Response
                    {
                        ServerResponseType = ServerResponse.SendNewMessages,
                        Chats = updatedChats
                    });

                // set chats as synchronized because 
                // we sent them to the client
                sender.SynchronizedChats = new List<Chat>(updatedChats.Clone());
            }

            // if the client already has all the messages
            else
            {
                // send null to show that we have nothing to say
                sender.ResponseQueue.Enqueue(
                    new Response
                    {
                        ServerResponseType = ServerResponse.SendNewMessages,
                        Chats = null
                    });
            }
        }

        // when user want send new message from client
        private void UserEnvironment_SendMessage(
            UserEnvironment sender, Request request)
        {
            // if user don't created return
            if (!sender.Authorized)
            {
                Console.WriteLine("User Don't Created");
                return;
            }

            // find author in db
            var authorEntityInDb = DataBaseContext.Users
                .Where((c) => c.Id == request.UserInitiator.Id)
                .ToList();

            // check if we don't find author with needed id in db
            if (authorEntityInDb.Count == 0)
            {
                Console.WriteLine("Needed Author Enitity Not Finded In Data Base");
                return;
            }

            // find needed chat
            var targetChatWithNeededId = DataBaseContext.Chats
                .Where((c) => c.Id == request.TargetChat.Id)
                .ToList();

            // check if we don't find chat with needed id
            if (targetChatWithNeededId.Count == 0)
            {
                Console.WriteLine("Needed Target Chat Not Finded");
                return;
            }

            // creating new message in data base context
            DataBaseContext.Messages.Add(
                new Message
                {
                    // extract the required author "User" entity
                    // it is assumed that there is only one result in the list
                    AuthorUser = authorEntityInDb[0],

                    // set creation time
                    CreatedAt = request.Message.CreatedAt,

                    // extract the required chat
                    // it is assumed that there is only one result in the list
                    ChatId = targetChatWithNeededId[0].Id,

                    // text of message
                    Text = request.Message.Text
                });

            // add this message to data base
            DataBaseContext.SaveChanges();

            // if user sen message we dont need sengin response, 
            // and flag waiting response stay false, fix this
            sender.WaitingRequest = true;
        }

        // when user want sign in
        private void UserEnvironment_SignIn(
            UserEnvironment sender, Request request)
        {
            // print for debug
            Console.WriteLine($"User Want Login! Login:{request.UserInitiator.Email} Password:{request.UserInitiator.Password}");

            // find instance of this user from server
            var findedUsers = DataBaseContext.Users
                .Where(c => c.Email == request.UserInitiator.Email && c.Password == request.UserInitiator.Password)
                .ToList();

            // if we don't finded user in db
            if (findedUsers.Count == 0)
            {
                // print for debug
                Console.WriteLine($"User with login {request.UserInitiator.Email} don't finded :(");

                // send "sign in is failed"
                sender.ResponseQueue.Enqueue(
                    new Response
                    {
                        ServerResponseType = ServerResponse.SignInFailed
                    });
                return;
            }
            // if we finded user
            else
            {
                sender.User = new User();
                sender.User = findedUsers[0];

                // print for debug
                Console.WriteLine($"User with login {request.UserInitiator.Email} and Password {request.UserInitiator.Password} finded! :)");

                // call a method that will trigger the desired event
                sender.SendAllUserInformation(sender.User);
            }
        }

        // if the user is successfully logged in, we send all his data
        private void UserEnvironment_SendAllUserInfoEvent(
            UserEnvironment sender, Request request)
        {
            // find all user chats
            var allTargeUserChats = DataBaseContext.Chats
                .Where(c => c.MemberUser.Id == sender.User.Id
                          || c.OwnerUser.Id == sender.User.Id)
                .ToList();

            // find all needed data for all of user chats 
            foreach (var item in allTargeUserChats)
            {
                // find all messages for current chat in db
                var messagesForCurrentChat = DataBaseContext.Messages
                                    .Where(m => m.ChatId == item.Id)
                                    .ToList();

                // find all members of current chat
                // so far, it is assumed that a chat 
                // can only have one member
                var membersOfCurrentChat = DataBaseContext.Chats
                                .Where(ch => ch.Id == item.Id)
                                .Select(r => r.MemberUser)
                                .ToList();

                // find all members of current chat
                // so far, it is assumed that a chat 
                // can only have one owner
                var ownersOfCurrentChat = DataBaseContext.Chats
                                .Where(ch => ch.Id == item.Id)
                                .Select(r => r.OwnerUser)
                                .ToList();
            }

            // send to User Environment
            // set the property, when installed it will 
            // automatically send a message to the client
            sender.ResponseQueue.Enqueue(
                new Response
                {
                    ServerResponseType = ServerResponse.SendAllInformation,
                    UserInfo = sender.User,
                    Chats = allTargeUserChats,
                });

            // set chats as synchronized because 
            // we sent them to the client
            sender.SynchronizedChats = new List<Chat>(allTargeUserChats.Clone());
        }

        #endregion

        #region Public Methods

        // accept in connection
        public void ListenIncomingConnections()
        {
            // create listener on needed ip and port
            TcpListener = new TcpListener(IPAddress, Port);

            // start listen incoming connections
            TcpListener.Start();

            Console.WriteLine("Start Listen");

            // accept incoming connection and continue listen
            while (true)
            {
                Console.WriteLine("Wait New Connection");
                // if user want connect save him in tcp client
                var newTcpClient = TcpListener.AcceptTcpClient();

                // create new user environment
                var userEnvironment =
                    new UserEnvironment(
                        _currentServer: this,
                        _tcpClient: newTcpClient);

                Console.WriteLine($"New Connection! User Enviroment Id: {userEnvironment.Id}");

                // setup for process new client
                Thread tcpClientThread = new Thread(
                    new ThreadStart(userEnvironment.Process));

                // for debug
                Console.WriteLine($"Count Of Server Users: {ServerUsers.Count}");

                // sub
                SubscribeOnUserEnvironmentEvents(userEnvironment);

                // start processing new client
                tcpClientThread.Start();

                //tcpClientThread.Join();
            }
        }

        // subscribe to events user environmen
        public void SubscribeOnUserEnvironmentEvents(
            UserEnvironment userEnvironment)
        {
            // when user want sign in
            userEnvironment.SignIn
                += UserEnvironment_SignIn;

            // when user want send new message from client
            userEnvironment.SendMessage
                += UserEnvironment_SendMessage;

            // if the user is successfully logged in, we send all his data
            userEnvironment.SendAllUserInfoEvent
                += UserEnvironment_SendAllUserInfoEvent;

            // when the client asks if there are new messages 
            // in the database that he does not have on the client
            userEnvironment.CheckNewMessageForMe
                += UserEnvironment_CheckNewMessageForMe;

            // when user want sign up
            userEnvironment.SignUp += (ss, ee) =>
                {
                    // if sender is not a UserEnviroment don't subscribe on his events
                    if (!(ss is UserEnvironment userSendedMessage))
                    {
                        Console.WriteLine("Sender is not a User Enviroment");
                        return;
                    }
                };

            // if the client program is not responding
            userEnvironment.Disconnected += (ss, ee) =>
                {
                    // if sender is not a UserEnviroment ignore this event
                    if (!(ss is UserEnvironment userSendedMessage))
                    {
                        Console.WriteLine("Sender is not a User Enviroment");
                        return;
                    }

                    Console.WriteLine("User Has Been Disconnected");

                    // remuve from server users list
                    ServerUsers.Remove(userSendedMessage);

                    // dispose current instance of UserEnviroment
                    userSendedMessage.Dispose();
                };

            // when user want create chat
            userEnvironment.CreateChat += async (ss, ee) =>
                {
                    //if sender is not a UserEnviroment don't subscribe on his events
                    if (!(ss is UserEnvironment userCreatedChat))
                    {
                        Console.WriteLine("Sender is not a User Enviroment");
                        return;
                    }

                    // if user don't created return
                    if (!userCreatedChat.Authorized)
                    {
                        Console.WriteLine("User Don't Created");
                        return;
                    }

                    // find chat partner 
                    var listChatPartner = DataBaseContext.Users
                        .Where(c => c.Nick == ee.ChatPartnerNickname)
                        .ToList();

                    // create new chat in data base
                    //DataBaseContext.Chats.Add(
                    //    new Chat
                    //    {
                    //        Members.Add(),
                    //        Owner = userCreatedChat.User,

                    //        // it is assumed that there is only one result in the list
                    //        Member = listChatPartner[0],
                    //        CreatedAt = ee.CreatedAt,
                    //        IsChannel = ee.IsChannel
                    //    });

                    // add this message to data base
                    await DataBaseContext.SaveChangesAsync();
                };

            // for debug
            Console.WriteLine("End Of Subscribe");
        } 

        /// <summary>
        /// Installed property <see cref="Chat.Owner"/> to <see cref="null"/> 
        /// in all chats in which the user is a owner.
        /// May be necessary for deletion from a database 
        /// that is not configured for cascading delete
        /// </summary>
        /// <param name="user">User you want to replace with null</param>
        /// <returns></returns>
        public bool SetOwnerPropertyToNullInChats(User user)
        {
            try
            {
                // find all chat where user owner
                var allOwnedChats = DataBaseContext.Chats
                    .Where(c => c.OwnerUser.Id == user.Id)
                    .ToList();

                // set all owned chat "owner" property to null
                allOwnedChats.ForEach(c => c.OwnerUser = null);
            }
            catch
            {
                // if it not works
                return false;
            }
            // if it works
            return true;
        }

        /// <summary>
        /// Installed property <see cref="Chat.Member"/> to <see cref="null"/> 
        /// in all chats in which the user is a member.
        /// May be necessary for deletion from a database 
        /// that is not configured for cascading delete
        /// </summary>
        /// <param name="user">User you want to replace with null</param>
        /// <returns></returns>
        public bool SetMemberPropertyToNullInChats(User user)
        {
            try
            {
                // find all chat where user owner
                var allOwnedChats = DataBaseContext.Chats
                    .Where(c => c.MemberUser.Id == user.Id)
                    .ToList();

                // set all owned chat "owner" property to null
                allOwnedChats.ForEach(c => c.MemberUser = null);
            }
            catch
            {
                // if it not works
                return false;
            }
            // if it works
            return true;
        }

        public void Dispose()
        {
            TcpListener.Stop();
            DataBaseContext.Dispose();
        }

        #endregion
    }

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
