using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Messenger
{
    public class DataBaseContext : DbContext
    {
        // current data base configuration, connection string etc.
        public IConfigurationRoot AppConfiguration { get; private set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageReaded> MessagesReaded { get; set; }

        public DataBaseContext()
        {
            // get json file properties in variable
            AppConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
