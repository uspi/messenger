using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Messenger.Core;
using Newtonsoft.Json;

namespace Messenger.Server
{
    /// <summary>
    /// The connected user's environment associates the 
    /// networking(<see cref="System.Net.Sockets.TcpClient"/>) component
    /// with the interaction with data on the <see cref="Server"/> 
    /// </summary>
    public class UserEnvironment : IDisposable
    {
        #region Public Events

        // if user want send a message
        public event EventHandler<SendMessageRequestEventArgs> SendMessage;

        // if user want let sign in
        public event EventHandler<SignInRequestEventArgs> SignIn;

        // if user want let sign in
        public event EventHandler<SignUpRequestEventArgs> SignUp;

        // if user want create new chat
        public event EventHandler<CreateNewChatRequestEventArgs> CreateChat;

        // if user want create new chat
        public event EventHandler<Response> SendAllUserInfoEvent;

        // if the client program is not responding
        public event EventHandler Disconnected;

        event EventHandler<ErrorEventArgs> HaveError;

        // if UserEnvronment noticed that new messages 
        // appeared on the server for us
        public event EventHandler CheckNewMessageForMe;

        // when the server responds to the request "CheNewMessagesForMe" 
        // and saves the data to "NewMessagesForMe", we notify current
        // UserEnvironment about this
        public event EventHandler NewMessagesForMeSaved;

        #endregion

        #region Public Properties

        // all current answers prepared for the client
        public Queue<Response> ResponseQueue { get; set; }

        // when we started we wait request from client
        public bool WaitingRequest { get; set; } = true;

        // id of current entity enviroment on server
        public string Id { get; set; }

        // if the user is authorized, he has a database context
        public User User { get; set; }

        // true if authorized
        public bool Authorized
        {
            get => User != null;
            set => Authorized = value;
        }

        #endregion

        // current tcp client connection 
        TcpClient TcpClient { get; set; }

        // stream between UserEnvironment and Client
        NetworkStream NetworkStream { get; set; }

        // server entity to which the client is connected
        Server CurrentServer { get; set; }

        // constructor
        public UserEnvironment(Server _currentServer, TcpClient _tcpClient)
        {
            // set unique id for current user environment
            Id = Guid.NewGuid().ToString();

            // set configutation for current entity
            TcpClient = _tcpClient;

            // set config for current entity
            CurrentServer = _currentServer;

            // add me to server user list
            CurrentServer.AddConnection(this);
        }

        // main method for this client, 
        public void Process()
        {
            Console.WriteLine("Get Network Stream");

            // create new response queue
            ResponseQueue = new Queue<Response>();

            // get newtwork stream of communication with the user
            NetworkStream = TcpClient.GetStream();

            // loop for receiving requests and initiating their processing
            while (true)
            {
                // if we do not wait for the request and have a ready response
                if (!WaitingRequest 
                    && ResponseQueue.Count > 0)
                {
                    WaitingRequest = true;

                    // remove and return object at the beginning of the Response Queue
                    var firstRequestInQueue = ResponseQueue.Dequeue();

                    // send to client server response
                    WriteToStreamAndSerialize(firstRequestInQueue);
                }

                // if true we are waiting for a client request
                if (WaitingRequest)
                {
                    // print for debug
                    Console.WriteLine("WaitRequest");

                    // read the message from the user and 
                    // deserialize it into the request
                    var requestObject = ReadFromStreamAndDeserialize();

                    // we received a request now it needs to be processed
                    WaitingRequest = false;

                    Console.WriteLine($"Waiting Request: {WaitingRequest}");

                    // print for debug
                    Console.WriteLine(
                        $"User Enviroment: {Id} Send New Request {requestObject.UserRequestType}");

                    // processing request
                    CheckRequestTypeAndWakeUpEventsForServer(requestObject);
                }
                
                
            }
        }

        // we check the type of request and form the data necessary 
        // for the server to respond to the request and send them in events
        public void CheckRequestTypeAndWakeUpEventsForServer(Request requestObject)
        {
            // satisfied user requests
            switch (requestObject.UserRequestType)
            {

                case UserRequest.SendMessage:
                    {
                        // wake up the event and pass the required 
                        // arguments to send the message
                        SendNewMessage(
                            targetChatId: requestObject.Message.ChatId,
                            targetText: requestObject.Message.Text);
                        break;
                    }

                case UserRequest.SignIn:
                    {
                        // wake up the event and pass the required 
                        // arguments to sign in
                        UserSignIn(
                            login: requestObject.UserInitiator.Email,

                            // char array in string
                            password: requestObject.UserInitiator.Password);
                        break;
                    }
                    
                case UserRequest.SignUp:
                    {
                        // wake up the event and pass the required 
                        // arguments to sign up
                        UserSignUp(
                            login: requestObject.UserInitiator.Email,

                            // char array in string
                            password: requestObject.UserInitiator.Password);
                        break;
                    }  

                case UserRequest.CreateNewChat:
                    {
                        // wake up the event and pass the required 
                        // arguments to create new chat
                        UserCreateNewChat(
                            chatPartnerNickname: requestObject.UserNickNameWantToFind,
                            chatOwnerId: requestObject.UserInitiator.Id,
                            chatOwnerNick: requestObject.UserInitiator.Nick);
                        break;
                    }

                default:
                    Console.WriteLine("Wrong Request Type");
                    break;
            }
        }

        #region Request Reaction Methods

        // if sign in, this first task
        public void SendAllUserInformation(User infoAboutUser)
        {
            // wake up the event send all user info
            SendAllUserInfoEvent(this,
                new Response
                {
                    UserInfo = infoAboutUser
                });
        }

        // wake up the event and pass the required arguments to send the message
        private void SendNewMessage(long targetChatId = 1, string targetText = "")
        {
            if (this.User == null)
            {
                Debug.WriteLine("User will be not created, return");
                return;
            }

            // wake up the event Send Message
            SendMessage(this,
                new SendMessageRequestEventArgs
                {
                    FromUserId = this.User.Id,
                    ToChatId = targetChatId,
                    CreatedAt = DateTimeOffset.UtcNow,
                    Text = targetText
                });
        }

        // wake up the event and pass the required arguments to user sign up
        private void UserSignUp(string login, string password, string nick = "Default_Nick")
        {
            if (this.User != null)
            {
                Debug.WriteLine("User has already been created");
                return;
            }

            // wake up the event Sign Up
            SignUp(this,
                new SignUpRequestEventArgs
                {
                    Login = login,
                    Password = password,
                    Nick = nick
                });
        }

        // wake up the event and pass the required arguments to user sign in
        private void UserSignIn(string login, string password)
        {
            if (this.User != null)
            {
                Debug.WriteLine("User has already been authorized");
                return;
            }

            // wake up the event Sign In
            SignIn(this,
                new SignInRequestEventArgs
                {
                    Login = login,
                    Password = password,
                });
        }

        // the user creates a new chat and adds a user with a nickname to it
        private void UserCreateNewChat(string chatPartnerNickname, long chatOwnerId, 
            string chatOwnerNick, bool isChannel = false)
        {
            CreateChat(this, 
                new CreateNewChatRequestEventArgs() 
                { 
                    ChatPartnerNickname = chatPartnerNickname,
                    CreatedAt = DateTimeOffset.UtcNow,
                    OwnerId = chatOwnerId,
                    OwnerNick = chatOwnerNick,
                    IsChannel = isChannel
                });
        }

        #endregion

        #region Stream Heplers

        /// <summary>
        /// Deserializes the request received from the user 
        /// and provides the <see cref="Request"/> object.
        /// </summary>
        /// <returns></returns>
        private Request ReadFromStreamAndDeserialize()
        {
            // create empty buffer
            byte[] buffer = new byte[4096];

            var receiveStringBuilder = new StringBuilder();

            // current position in the NetworkStream
            int streamPosition = 0;

            Console.WriteLine($"Start Read, Stream Position: {streamPosition}");
            Console.WriteLine($"Waiting Request: {WaitingRequest}");

            do
            {
                streamPosition = NetworkStream.Read(buffer, 0, buffer.Length);

                // encoding one character at a time and appending 
                // it to the incoming receive string
                receiveStringBuilder.Append(
                    Encoding.UTF8.GetString(buffer, 0, streamPosition));
                
            } while (NetworkStream.DataAvailable);

            Console.WriteLine($"Read Done, Stream Position: {streamPosition}");

            // convert bytes to string
            var jsonString = receiveStringBuilder.ToString();

            //Console.WriteLine($"Message: {jsonString}");

            Console.WriteLine("Start Deserialize");

            // deserialize json string to request
            return JsonConvert.DeserializeObject<Request>(jsonString);
        }

        /// <summary>
        /// Serializes and sends sever <see cref="Response"/> 
        /// on the user request to the <see cref="NetworkStream"/>
        /// </summary>
        /// <param name="serverResponse">Object of server Response</param>
        /// <returns></returns>
        public void WriteToStreamAndSerialize(Response serverResponse)
        {
            // serialize object to json string
            string jsonString = JsonConvert.SerializeObject(serverResponse);

            // encoding string to bytes
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

            // write bytes in network stream
            NetworkStream.Write(jsonBytes, 0, jsonBytes.Length);
        }

        #endregion

        public void Dispose()
        {
            NetworkStream.Dispose();
            TcpClient.Dispose();
        }
    }
}
