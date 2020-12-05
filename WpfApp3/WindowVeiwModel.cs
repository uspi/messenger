using PropertyChanged;
using System.Windows;
using System.Windows.Input;

namespace WPFClient
{
    [AddINotifyPropertyChangedInterface]
    public class WindowViewModel : Notifier
    {
        // Window this view model controls
        private Window mWindow;

        // The margin around the window to allow for a drop shadow
        private int mOuterMarginSize = 10;

        // Corner radius of the window
        private int mWindowRadius = 10;


        // The size of the resize border around the window
        public int ResizeBorder { get; set; } = 6;

        // The size of the resize border around the window, taking into account the outer margin
        public Thickness ResizeBorderThickness { get => new Thickness(ResizeBorder + OuterMarginSize); }

        // The margin around the window to allow for a drop shadow
        public int OuterMarginSize
        {
            get => (mWindow.WindowState == WindowState.Maximized) ? 0 : mOuterMarginSize;
            set => mOuterMarginSize = value;
        }

        // The margin around the window to allow for a drop shadow
        public Thickness OuterMarginSizeThickness { get => new Thickness(OuterMarginSize); }

        // Corner radius of the window
        public int WindowRadius
        {
            get => (mWindow.WindowState == WindowState.Maximized) ? 0 : mWindowRadius;
            set => mWindowRadius = value;
        }

        // Corner radius of the window
        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }

        //height of the title bar window
        public int TitleHeight { get; set; } = 16;

        public GridLength TitleHeightGridLength { get => new GridLength(TitleHeight + ResizeBorder); }

        public ICommand WindowMinimizeCommand { get; set; }
        public ICommand WindowMaximizeCommand { get; set; }
        public ICommand WindowCloseCommand { get; set; }
        public ICommand ShowWindowMenuCommand { get; set; }

        public WindowViewModel(Window window)
        {
            mWindow = window;

            // View model event, listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            // command initialization
            WindowMinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            WindowMaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            WindowCloseCommand = new RelayCommand(() => mWindow.Close());
            ShowWindowMenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));
        }

        #region LoginScreen
        private Messenger messenger;

        public string MainLabelText { get; set; } = "Login";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        public int LoginMaxLength { get; set; } = 32;
        public int PasswordMaxLength { get; set; } = 32;

        public bool DevLineVisibility { get; set; } = false;
        public bool LoginScreenVisibility { get; set; } = false;

        public ICommand UserAuthorizationCommand { get; set; }


        //this line shuld be in constructor, if not will be issue
        //UserAuthorizationCommand = new RelayCommand(UserAuthorization);

        private void UserAuthorization()
        {
            if (messenger != null)
            {
                messenger.Auth();
            }
            else
            {
                messenger = new Messenger(Login, Password);
                messenger.Connect();
                messenger.Auth();
            }
        }
        #endregion

        #region Private Helpers
        // gets the current mouse position on the screen
        private Point GetMousePosition()
        {
            // postition of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }
        #endregion
    }
}
