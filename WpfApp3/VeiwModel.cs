using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    //[ImplementPropertyChanged]
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        string nameViewMode = "ViewModel";
        string NameViewMode { get => nameViewMode; }
        
        private object currentViewModel;
        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set 
            { 
                currentViewModel = value;
                //OnPropertyChanged("CurrentViewModel");
            }
        }

        private List<object> viewModelPages = new List<object>();
        public List<object> ViewModelPages
        {
            get { return viewModelPages; }
            set 
            { 
                viewModelPages = value;
                //OnPropertyChanged("ViewModelPages");
            }
        }

        private object _applicationDataContext;
        
        private int passwordMaxLength = 32;
        private string password = "";
        private string login = "";
        private string mainLabelText = "Login";
        private Messenger messenger;
        private RelayCommand userAuthorization;
        private bool devLineVisibility = false;
        
        private bool loginScreenVisibility = true;

        public string MainLabelText
        {
            get { return mainLabelText; }
            set
            {
                mainLabelText = value;
                //OnPropertyChanged("MainLabelText");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                //OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length <= 32) password = value;
                //OnPropertyChanged("Password");
            }
        }
        public int PasswordMaxLength
        {
            get { return passwordMaxLength; }
            set
            {
                passwordMaxLength = value;
                //OnPropertyChanged("PasswordMaxLength");
            }
        }
        public bool DevLineVisibility
        {
            get { return devLineVisibility; }
            set 
            { 
                devLineVisibility = value;
                //OnPropertyChanged("DevLineVisibility");
            }
        }
        public bool LoginScreenVisibility
        {
            get { return loginScreenVisibility; }
            set 
            { 
                loginScreenVisibility = value;
                //OnPropertyChanged("LoginScreenVisibility");
            }
        }

        public ViewModel(object applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
        }

        public RelayCommand UserAuthorization
        {
            get
            {
                return userAuthorization = new RelayCommand(obj =>
                {
                    LoginScreenVisibility = false;
                });
            }
            //get
            //{
            //    if (userAuthorization != null)
            //    {
            //        messenger.Auth();
            //    }
            //    return userAuthorization = new RelayCommand(obj =>
            //    {
            //        messenger = new Messenger(login, password);
            //        messenger.Connect();
            //        messenger.Auth();
            //    });
            //}
        }
    }
}
