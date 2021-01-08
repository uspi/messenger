using System.Windows.Input;
using System.Threading.Tasks;

namespace Messenger.Core
{
    /// <summary>
    /// Interactive logic of sign in screen
    /// </summary>
    public class SignInViewModel : ViewModelBase
    {
        #region Public Properties

        // user login
        public string Login { get; set; }

        // indicating if the user sign in command is running
        public bool SignInIsRunning { get; set; }

        #endregion

        #region Commands

        // login command
        public ICommand SignInCommand { get; set; }

        // sign up command
        public ICommand SignUpCommand { get; set; }

        #endregion

        #region Constructor
        public SignInViewModel()
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
            IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.SignUp;

            await Task.Delay(50);
        }
    }
}

