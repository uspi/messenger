using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messenger.Core
{
    public class Chat : ICloneable
    {
        [Key]
        public long Id { get; set; }

        public User MemberUser { get; set; }

        public User OwnerUser { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public bool IsChannel { get; set; }

        public IList<Message> Messages { get; set; }

        // clone this chat
        public object Clone()
        {
            
            // in order to use the cloning 
            // method for each message instance
            var messages = new List<Message>();

            // if messages count not 0
            if (Messages != null)
            {
                // call the clone method on each instance of the sequence
                foreach (var message in Messages)
                {
                    // clone this message and add in list
                    messages.Add((Message)message.Clone());
                }
            }
            

            return 
                new Chat 
                { 
                    Id = this.Id,
                    MemberUser = this.MemberUser,
                    OwnerUser = this.OwnerUser,
                    CreatedAt = this.CreatedAt,
                    IsChannel = this.IsChannel,
                    Messages = messages
                };
        }
    }
}
