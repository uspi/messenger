using System.Windows.Input;
using System.Threading.Tasks;

namespace Messenger.Core
{
    /// <summary>
    /// Interactive logic of sign up screen
    /// </summary>
    public class SignUpViewModel : ViewModelBase
    {
        #region Public Properties

        // user login
        public string Login { get; set; }

        // indicating if the user sign up command is running
        public bool SignUpIsRunning { get; set; }

        #endregion

        #region Commands

        // login command
        public ICommand SignInCommand { get; set; }

        // sign up command
        public ICommand SignUpCommand { get; set; }

        #endregion

        #region Constructor
        public SignUpViewModel()
        {
            SignUpCommand = new RelayParameterizedCommand(async (parameter) 
                => await SignUpAsync(parameter));

            SignInCommand = new RelayCommand(async () 
                => await SignInAsync());
        }


        #endregion

        // attempts to user sign up, parameter - secure string(user password)
        public async Task SignUpAsync(object parameter)
        {
            await RunCommand(() => this.SignUpIsRunning, async () =>
            {
                await Task.Delay(500);

                // go to chat page
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Chat);

                //string login = this.Login;
                //// NOT STORE UNSECURE PASSWORD IN VARIABLE, it's for test
                //string password = (parameter as IHavePassword).SecurePassword.Unsecure();
            });  
        }

        // takes the user to the sign in page
        public async Task SignInAsync()
        {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.SignIn);

            await Task.Delay(50);
        }
    }
}

