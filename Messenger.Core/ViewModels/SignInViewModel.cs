using System.Windows.Input;
using System.Threading.Tasks;
using System;

namespace Messenger.Core
{
    /// <summary>
    /// Interactive logic of sign in screen
    /// </summary>
    public class SignInViewModel : ViewModelBase
    {
        #region Public Events

        // wake up when im want sign in
        public event EventHandler<Request> SignInFromViewModel;

        #endregion

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
            // if the authorization process is already in progress
            if (SignInIsRunning)
            {
                // exit from method
                return;
            }

            // if user not write login set to none parameter
            string loginData = Login == null ? "none" : Login;

            // in order not to switch the execution context in the event, to change the page
            bool signInFailed = false;

            // true if we have server response
            bool haveResult = false;

            await RunCommand(() => this.SignInIsRunning, async () =>
            { 

                // subsribe on sign in done event
                IoC.Get<NetworkConnection>().SignInFail += (response) =>
                {
                    // we got the query result
                    haveResult = true;

                    // set flag to true
                    signInFailed = true;

                    // TODO: action if we failed
                };

                // subsribe on sign in done event
                IoC.Get<NetworkConnection>().SignInDone += (response) =>
                {
                    // we got the query result
                    haveResult = true;

                    // set flag to true
                    signInFailed = false;

                    // TODO: action if we done
                };

                // wake up event and give him login and password
                SignInFromViewModel(this,
                    new Request
                    {
                        UserRequestType = UserRequest.SignIn,
                        UserInitiator =
                            new User
                            {
                                Email = loginData,
                                Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                            }
                    });

                // wait result
                while (!haveResult)
                {
                    await Task.Delay(200);
                } 
            });

            // if sign in failed
            if (signInFailed)
            {
                // for next try
                signInFailed = false;
                return;
            }

            // else go to chat page
            IoC.Get<ApplicationViewModel>()
                .GoToPage(ApplicationPage.Chat, 
                    new ChatMessageListViewModel 
                    { 
                        DisplayTitle = "Signed From Server",

                        // we go to the chat page, but we want it to 
                        // be just an empty space and not an empty chat
                        IsBootScreenStub = true 
                    });
        }

        // takes the user to the sign up page
        public async Task SignUpAsync()
        {
            // open a sign up page
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.SignUp);

            await Task.Delay(50);
        }
    }
}

