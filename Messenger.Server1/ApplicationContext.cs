using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Messenger
{
    public class OnStartUp
    {
        // current server instance
        public static ServerBase Server { get; set; }

        static void Main()
        {
            // create server instance
            Server = new ServerBase();

            //Console.WriteLine(context.AppConfiguration.GetConnectionString("DefaultConnection"));
        }
    }

    public class ServerBase
    {
        public DataBaseContext CurrentDataBaseContext { get; set; }

        public ServerBase()
        {
            // check if we dont have data base context
            if (CurrentDataBaseContext == null)
            {
                CurrentDataBaseContext = new DataBaseContext();
            }
        }

        public void CreateUser()
        {
            var newUser = new User
            {
                Email = "bran@gmail.com",
                Password = 123456,
                FirstName = "Bran",

            }
        }
    }
}
