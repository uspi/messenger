using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger
{
    public class User
    {
        public int Id { get; set; }
        
        public string Email { get; set; }

        public char[] Password { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Nick { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
