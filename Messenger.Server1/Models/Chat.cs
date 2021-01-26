using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger
{
    public class Chat
    {
        public long Id { get; set; }

        public User MemberId { get; set; }

        public User OwnerId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool IsChannel { get; set; }
    }
}
