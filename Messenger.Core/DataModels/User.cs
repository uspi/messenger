using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Core
{
    /// <summary>
    /// Data model for presenting a user to a server
    /// </summary>
    public class User : ICloneable
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(32)]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [MaxLength(32)]
        [Required]
        public string Nick { get; set; }

        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
