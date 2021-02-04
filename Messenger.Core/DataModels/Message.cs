using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Core
{
    public class Message : ICloneable
    {
        [Key]
        public long Id { get; set; }

        public long ChatId { get; set; }

        public User AuthorUser { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [StringLength(4096)]
        public string Text { get; set; }

        public object Clone()
        {
            return 
                new Message 
                { 
                    Id = this.Id,
                    ChatId = this.ChatId,

                    // clone author user entity
                    AuthorUser = (User)AuthorUser.Clone(),

                    CreatedAt = this.CreatedAt,
                    Text = this.Text
                };
        }
    }
}
