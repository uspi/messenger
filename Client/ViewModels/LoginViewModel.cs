using System.Windows.Input;
using System.Threading.Tasks;

namespace WPFClient
{
    /// <summary>
    /// Interactive logic of Login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        // user login
        public string Login { get; set; }

        // indicating if the user authorization command is running
        public bool AuthorizationIsRunning { get; set; }

        #endregion

        #region Commands

        // login command
        public ICommand SignInCommand { get; set; }

        // sign up command
        public ICommand SignUpCommand { get; set; }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            SignInCommand = new RelayParameterizedCommand(async (parameter) 
                => await SignInAsync(parameter));

            SignUpCommand = new RelayCommand(async () 
                => await SignUpAsync());
        }


        #endregion

        // attempts to log the user in, parameter - secure string(user password)
        public async Task SignInAsync(object parameter)
        {
            await RunCommand(() => this.AuthorizationIsRunning, async () =>
            {
                await Task.Delay(5000);

                string login = this.Login;
                // NOT STORE UNSECURE PASSWORD IN VARIABLE, it's for test
                string password = (parameter as IHavePassword).SecurePassword.Unsecure();
            });  
        }

        // takes the user to the sign up page
        public async Task SignUpAsync()
        {
            await Task.Delay(50);
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

        //public ICommand SignInCommand { get; set; }

        ////this line should be in the constructor, if not will be issue
        ////SignInCommand = new RelayCommand(SignIn);

        //private void SignIn()
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

