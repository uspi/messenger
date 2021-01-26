using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger
{
    public class Message
    {
        public int Id { get; set; }

        public Chat ToChat { get; set; }

        public User FromUser { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Text { get; set; }
    }
}
