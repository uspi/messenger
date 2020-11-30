using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.MessengerItems
{
    class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string prop = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  
        public ObservableCollection<Message> messages;
        public ObservableCollection<Message> channels;
        public ObservableCollection<Contact> contacts;

        //notifications about changes to the fields below are not implemented
        public string login;
        public string password;
        public string name;
        public string status;
        public int id;

        public User(string _login, string _password)
        {
            login = _login;
            password = _password;
        }  
    }
}
