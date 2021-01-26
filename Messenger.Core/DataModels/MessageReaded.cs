using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Core
{
    /// <summary>
    /// Data model for presenting a Readed Messages
    /// </summary>
    public class MessageReaded
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Message Message { get; set; }

        // user who readed
        [Required]
        public User User { get; set; }

        public DateTimeOffset ReadedAt { get; set; }
    }
}
