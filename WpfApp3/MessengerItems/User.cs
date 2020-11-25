using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.MessengerItems
{
    class User
    {
        //поля для синхронизации с базой данных
        public int id;
        public string login;
        public string password;
        public string name;
        public string status;
        List<Message> messages;
        List<Message> channels;
        List<Contact> contacts;
        public User(string _login, string _password)
        {
            login = _login;
            password = _password;
        }
    }
}
