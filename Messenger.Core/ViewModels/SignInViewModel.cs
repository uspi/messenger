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
            // in order not to switch the execution context in the event, to change the page
            bool signInFailed = false;

            await RunCommand(() => this.SignInIsRunning, async () =>
            {
                // boolean result of this task
                var tcs = new TaskCompletionSource<bool>();

                #region Debug Block

                //// go to chat page
                //IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel { DisplayTitle = "Signed From Server" });

                //tcs.SetResult(true);

                #endregion

                #region Code Block

                // subsribe on sign in done event
                IoC.Get<NetworkConnection>().SignInFail += (response) =>
                {
                    // set flag to true
                    signInFailed = true;

                    // TODO: action if we failed

                    tcs.SetResult(false);
                };

                // subsribe on sign in done event
                IoC.Get<NetworkConnection>().SignInDone += (response) =>
                {
                    // set flag to true
                    signInFailed = false;

                    // TODO: action if we done

                    tcs.SetResult(true);
                };

                // wake up event and give him login and password
                SignInFromViewModel(this,
                    new Request
                    {
                        UserRequestType = UserRequest.SignIn,
                        UserInitiator =
                            new User
                            {
                                Email = Login,
                                Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                            }
                    });

                #endregion

                //SignInFromViewModel(this, new Request());

                // await in current context (gui thread)
                await tcs.Task;
            });

            // if sign in failed
            if (signInFailed)
            {
                return;
            }

            //IoC.Get<ChatListDesignModel>().SetItems();

            // else go to chat page
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel { DisplayTitle = "Signed From Server", IsBootScreenStub = true });
        }

        // takes the user to the sign up page
        public async Task SignUpAsync()
        {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.SignUp);

            await Task.Delay(50);
        }
    }
}

