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
            // todo : tcp ip connect
            await RunCommand(() => this.SignInIsRunning, async () =>
            {
                await Task.Delay(1000);

                // go to chat page
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Chat);

                //string login = this.Login;
                //// NOT STORE UNSECURE PASSWORD IN VARIABLE, it's for test
                //string password = (parameter as IHavePassword).SecurePassword.Unsecure();
            });  
        }

        // takes the user to the sign up page
        public async Task SignUpAsync()
        {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.SignUp);

            await Task.Delay(50);
        }
    }
}

