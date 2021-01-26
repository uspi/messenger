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

        public User Member { get; set; }

        public User Owner { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public bool IsChannel { get; set; }

        public List<Message> Messages { get; set; }
    }
}
