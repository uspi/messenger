using System.Data.Entity;
using Messenger.Core;

namespace Messenger.Server
{
    /// <summary>
    /// Represents the layer between the <see cref="Server"/> and the database. 
    /// Stores data base tables in <see cref="DbSet"/>
    /// </summary>
    public class DataBaseContext : DbContext
    {
        // table Users
        public DbSet<User> Users { get; set; }

        // table Chats
        public DbSet<Chat> Chats { get; set; }

        // table Messages
        public DbSet<Message> Messages { get; set; }

        // table MessagesReaded
        public DbSet<MessageReaded> MessReaded { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }


        // current data base configuration, connection string etc.
        //public IConfigurationRoot AppConfiguration { get; private set; }

        //public DataBaseContext()
        //{
        //    // get json file properties in variable
        //    AppConfiguration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //}
    }
}
