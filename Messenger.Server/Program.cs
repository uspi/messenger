using System;
using System.Net;
using System.Threading;

namespace Messenger.Server
{
    /// <summary>
    /// Actions when application starts
    /// </summary>
    class Program
    {
        static void Main()
        {
            // create application instace
            _ = new Application();

            Console.WriteLine("Application Was End Working");

            Console.Read();
        }
    }

    /// <summary>
    /// All application logic
    /// </summary>
    public class Application
    {
        #region Public Properties

        // instace of server of application
        public static Server CurrentServer { get; set; }

        // thread in which to listen for incoming connections
        public static Thread ListenThread { get; set; }

        #endregion

        #region Constructor

        public Application()
        {
            Inizialize();
        }

        #endregion

        #region Private Methods

        private void Inizialize()
        {
            // create data base context, configuration 
            // for this in app.config
            var context = new DataBaseContext();

            // server setup
            CurrentServer = new Server(
                ipAddress: IPAddress.Parse("127.0.0.1"),
                port: 1300,

                repository: new Repository(context));

            #region Debug Stuff
            //// create user
            //CurrentServer.DataBaseContext.Users.Add(
            //    new User
            //    {
            //        Email = "rob@gmail.com",
            //        Password = "qwerty",
            //        CreatedAt = DateTimeOffset.UtcNow,
            //        Nick = "rob",
            //        FirstName = "RobFirstName",
            //        LastName = "RobLastName"
            //    });

            //CurrentServer.DataBaseContext.SaveChanges();

            //// find created user
            //var neededUser = CurrentServer.DataBaseContext.Users.Where(u => u.Nick == "rob").FirstOrDefault();

            //// find created user
            //var memberUser = CurrentServer.DataBaseContext.Users.Where(u => u.Nick == "ben").First();

            // find chat by id
            //var neededChat = CurrentServer.DataBaseContext.Chats.Where(c => c.Id == 1).FirstOrDefault();

            //neededChat.MemberUser = admin;

            //CurrentServer.DataBaseContext.SaveChanges();

            // find all chat where user owner
            //var ownedChats = CurrentServer.DataBaseContext.Chats.Where(c => c.Owner.Id == admin.Id).ToList();

            // find all chat where user member

            // delete created user
            //CurrentServer.DataBaseContext.Users.Remove(admin);

            // create chat
            //var chat = CurrentServer.DataBaseContext.Chats.Add(
            //    new Chat
            //    {
            //        CreatedAt = DateTimeOffset.UtcNow,
            //        IsChannel = false,
            //        OwnerUser = neededUser,
            //        MemberUser = memberUser
            //    });

            //CurrentServer.DataBaseContext.SaveChanges();

            // create message
            //CurrentServer.DataBaseContext.Messages.Add(
            //    new Message
            //    {
            //        AuthorUser = admin,
            //        CreatedAt = DateTimeOffset.UtcNow,
            //        ChatId = 1,
            //        Text = "Hello, I am here!"
            //    });

            //CurrentServer.DataBaseContext.SaveChanges();

            //var aaa = CurrentServer.DataBaseContext.Messages.Where(c => c.Id == 100).FirstOrDefault(); 
            #endregion

            // write on screen server ip
            Console.WriteLine($"Server IPAdress: {CurrentServer.IPAddress}");

            // write on screen server port
            Console.WriteLine($"Server Port: {CurrentServer.Port}");

            // write on screen what are we waiting for connection
            Console.WriteLine("Started Listen Icoming Connections");

            // setup for listening thread
            ListenThread = new Thread(

                // start thread for listening incoming connection
                new ThreadStart(CurrentServer.ListenIncomingConnections));

            // start listening
            ListenThread.Start();

            // dont close program before closin listen thread
            ListenThread.Join();

            Console.WriteLine("Server Was End Working");
        }  

        #endregion
    }
}
