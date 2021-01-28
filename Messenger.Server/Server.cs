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

        #region Methods

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

                SubscribeOnUserEnviromentEvents(userEnvironment);

                // start processing new client
                tcpClientThread.Start();

                tcpClientThread.Join();
            }

            
        }

        // subscribe to events and add to the server list
        public void SubscribeOnUserEnviromentEvents(UserEnvironment userEnvironment)
        {
            try
            {
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

                    // dispose current instance of UserEnviroment
                    userSendedMessage.Dispose();
                };

                // when user want sign in
                userEnvironment.SignIn += (ss, ee) =>
                {
                    // cast sender to user evironment
                    var signInUser = (UserEnvironment)ss;
                    var request = (SignInRequestEventArgs)ee;

                    // print for debug
                    Console.WriteLine($"User Want Login! Login:{ee.Login} Password:{ee.Password}");

                    // find instance of this user from server
                    var findedUsers = DataBaseContext.Users
                        .Where(c => c.Email == ee.Login && c.Password == ee.Password)
                        .ToList();

                    // if we don't finded user in db
                    if (findedUsers.Count == 0)
                    {
                        // print for debug
                        Console.WriteLine($"User with login {ee.Login} don't finded :(");

                        // send "sign in is failed"
                        signInUser.ResponseQueue.Enqueue(
                        new Response
                        {
                            ServerResponseType = ServerResponse.SignInFailed
                        });
                        return;
                    }
                    // if we finded user
                    else 
                    {
                        signInUser.User = new User();
                        signInUser.User = findedUsers[0];

                        // print for debug
                        Console.WriteLine($"User with login {ee.Login} and Password {ee.Password} finded! :)");

                        // call a method that will trigger the desired event
                        signInUser.SendAllUserInformation(signInUser.User);
                    }     
                };

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

                // when user want send message
                userEnvironment.SendMessage += async (ss, ee) =>
                {
                    //if sender is not a UserEnviroment don't subscribe on his events
                    if (!(ss is UserEnvironment userSendedMessage))
                    {
                        Console.WriteLine("Sender is not a User Enviroment");
                        return;
                    }

                    // if user don't created return
                    if (!userSendedMessage.Authorized)
                    {
                        Console.WriteLine("User Don't Created");
                        return;
                    }

                    // find author in db
                    var authorEntityInDb = DataBaseContext.Users
                        .Where((c) => c.Id == ee.FromUserId)
                        .ToList();

                    // check if we don't find author with needed id in db
                    if (authorEntityInDb.Count == 0)
                    {
                        Console.WriteLine("Needed Author Enitity Not Finded In Data Base");
                        return;
                    }

                    // find needed chat
                    var targetChatWithNeededId = DataBaseContext.Chats
                        .Where((c) => c.Id == ee.ToChatId)
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
                            // extract the required chat
                            // it is assumed that there is only one result in the list
                            Id = targetChatWithNeededId[0].Id,

                            // extract the required author "User" entity
                            // it is assumed that there is only one result in the list
                            AuthorUser = authorEntityInDb[0],
                            CreatedAt = ee.CreatedAt,
                            Text = ee.Text
                        });

                    // add this message to data base
                    await DataBaseContext.SaveChangesAsync();

                    // find target User Enviroment in server connecitons
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

                // if the user is successfully logged in, we send all his data
                userEnvironment.SendAllUserInfoEvent += (ss, ee) =>
                {
                    var targetUser = (UserEnvironment)ss;

                    // find all user chats
                    var allTargeUserChats = DataBaseContext.Chats
                        .Where(c => c.MemberUser.Id == targetUser.User.Id
                                  || c.OwnerUser.Id == targetUser.User.Id)
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
                    }

                    // send to User Environment
                    // set the property, when installed it will 
                    // automatically send a message to the client
                    targetUser.ResponseQueue.Enqueue(
                        new Response
                        {
                            ServerResponseType = ServerResponse.SendAllInformation,
                            UserInfo = targetUser.User,
                            Chats = allTargeUserChats,
                        });
                };

                // for debug
                Console.WriteLine($"Count Of Server Users: {ServerUsers.Count}");
            }
            catch (Exception hadledExeption)
            {
                // for debug
                Console.WriteLine($"Error in ProcessNewClient async task. + " +
                    $"User Disconnected from server{hadledExeption.ToString()}");

                // dispose current instance of UserEnviroment
                userEnvironment.Dispose();
            }

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

        public void SendUserResponse(UserEnvironment userEnvironment, Response response)
        {
            userEnvironment.WriteToStreamAndSerialize(response);
        }

        public void Dispose()
        {
            TcpListener.Stop();
            DataBaseContext.Dispose();
        }

        #endregion
    }

    public static class ServerExtensionHelpers
    {
        /// <summary>
        /// Adds a new user to the list of <see cref="Server.ServerUsers"/> 
        /// connected to the server
        /// </summary>
        /// <param name="server">Server for user connection</param>
        /// <param name="userEnvironment">The user to add</param>
        public static void AddConnection(this Server server, UserEnvironment userEnvironment)
        {
            server.ServerUsers.Add(userEnvironment);
        }

        /// <summary>
        /// Removes a user from the <see cref="Server.ServerUsers"/> of current users
        /// </summary>
        /// <param name="server">Server for user connection</param>
        /// <param name="userEnvironment">The user to add</param>
        public static void RemoveConnection(this Server server, UserEnvironment userEnvironment)
        {
            if (userEnvironment == null)
            {
                return;
            }

            server.ServerUsers.Remove(userEnvironment);
        }
    }
}
