using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messenger.Core
{
    public class Chat
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
    }
}
