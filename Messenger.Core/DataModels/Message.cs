using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Core
{
    public class Message
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Chat TargetChat { get; set; }

        public User AuthorUser { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [StringLength(4096)]
        public string Text { get; set; }
    }
}
