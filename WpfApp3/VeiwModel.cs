using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClient.MessengerItems;

namespace WPFClient
{
    class ViewModel : INotifyPropertyChanged
    {
        private object _applicationDataContext;
        //public ObservableCollection<bool> screens

        public event PropertyChangedEventHandler PropertyChanged;
        
        private bool devLineVisibility;
        private int passwordMaxLength = 32;
        private string password = "";
        private string login = "";
        private string mainLabelText = "Login";
        private Messenger messenger;
        private RelayCommand userAuthorization;   
        
        public string MainLabelText
        {
            get { return mainLabelText; }
            set
            {
                mainLabelText = value;
                OnPropertyChanged("MainLabelText");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length <= 32) password = value;
                OnPropertyChanged("Password");
            }
        }
        public int PasswordMaxLength
        {
            get { return passwordMaxLength; }
            set
            {
                passwordMaxLength = value;
                OnPropertyChanged("PasswordMaxLength");
            }
        }
        public bool DevLineVisibility
        {
            get { return devLineVisibility; }
            set { devLineVisibility = value;
                OnPropertyChanged("DevLineVisibility");
            }
        }
        
        public ViewModel(object applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
        }

        public RelayCommand UserAuthorization
        {
            //get
            //{  
            //    return userAuthorization = new RelayCommand(obj =>
            //    {
            //        Login = "hello";
            //        //DevLineVisibility = true;
            //    });
            //}
            get
            {
                if (userAuthorization != null)
                {
                    messenger.Auth();
                }
                return userAuthorization = new RelayCommand(obj =>
                {
                    messenger = new Messenger(login, password);
                    messenger.Connect();
                    messenger.Auth();
                });
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
