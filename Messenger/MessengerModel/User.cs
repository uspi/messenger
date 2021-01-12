using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Messenger
{
    class User
    {
        //public ObservableCollection<Message> messages;
        //public ObservableCollection<Message> channels;
        //public ObservableCollection<Contact> contacts;

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }  
    }
}
