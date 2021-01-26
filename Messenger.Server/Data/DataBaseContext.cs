using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
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
        //{
        //    get => Users;
        //    set
        //    {
        //        Users = value;

        //        // when have new data, update in the db
        //        this.SaveChanges();
        //    }
        //}

        // table Chats
        public DbSet<Chat> Chats { get; set; }
        //{
        //    get => Chats;
        //    set
        //    {
        //        Chats = value;

        //        // when have new data, update in the db
        //        this.SaveChanges();
        //    }
        //}

        // table Messages
        public DbSet<Message> Messages { get; set; }
        //{
        //    get => Messages;
        //    set
        //    {
        //        Messages = value;

        //        // when have new data, update in the db
        //        this.SaveChanges();
        //    }
        //}

        // table MessagesReaded
        public DbSet<MessageReaded> MessReaded { get; set; }
        //{
        //    get => MessReaded; 
        //    set 
        //    { 
        //        MessReaded = value; 

        //        // when have new data, update in the db
        //        this.SaveChanges(); 
        //    } 
        //}

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
