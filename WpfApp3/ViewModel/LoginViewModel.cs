using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using System;

namespace WPFClient
{
    /// <summary>
    /// View Model for Login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        #region Public Properties

        // user login
        public string Login { get; set; }

        #endregion

        #region Commands

        // login command
        public ICommand UserAuthorizationCommand { get; set; }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            UserAuthorizationCommand = new RelayParameterizedCommand(
                async (parameter) => await UserAuthorization(parameter));
        }


        #endregion

        // attempts to log the user in, parameter - secure string(user password)
        public async Task UserAuthorization(object parameter)
        {
            await Task.Delay(500);
        }

        #region LoginScreen
        //private Messenger messenger;

        //public string MainLabelText { get; set; } = "Login";

        //// default values for login fields
        //public string Login { get; set; } = "";
        //public string Password { get; set; } = "";  

        //// length of login fields
        //public int LoginMaxLength { get; set; } = 32;
        //public int PasswordMaxLength { get; set; } = 32;

        //public bool DevLineVisibility { get; set; } = false;

        //// view visibility for this viewmodel
        //public bool LoginScreenVisibility { get; set; } = false;

        //public ICommand UserAuthorizationCommand { get; set; }

        ////this line should be in the constructor, if not will be issue
        ////UserAuthorizationCommand = new RelayCommand(UserAuthorization);

        //private void UserAuthorization()
        //{
        //    if (messenger != null)
        //    {
        //        messenger.Auth();
        //    }
        //    else
        //    {
        //        messenger = new Messenger(Login, Password);
        //        messenger.Connect();
        //        messenger.Auth();
        //    }
        //}
        #endregion
    }
}

