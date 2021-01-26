using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger
{
    public class MessageReaded
    {
        public Message Message { get; set; }

        public User ReadedBy { get; set; }

        public DateTimeOffset ReadedAt { get; set; }
    }
}
