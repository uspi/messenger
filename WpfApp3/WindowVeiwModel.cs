
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Security;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WPFClient
{
    public class WindowViewModel : Notifier
    {
        internal CurrentOS _CurrentOS { get; } = new CurrentOS();

        // Window this view model controls
        private Window mWindow;

        // the margin around the window to allow for a drop shadow
        private int mOuterMarginSize = 0;//10

        // Corner radius of the window
        private int mWindowRadius = 0;

        private int resizeBorder = 6;
        private int titleHeight = 22;

        private WindowDockPosition mDockPosition { get; set; } = WindowDockPosition.Undocked;

        #region Public Properties

        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMinimumHeight { get; set; } = 450;

        // The margin around the window to allow for a drop shadow
        public int OuterMarginSize
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : mOuterMarginSize;
            }

            set => mOuterMarginSize = value;
        }

        // Corner radius of the window
        public int WindowRadius
        {
            get =>
                (mWindow.WindowState == WindowState.Maximized) ?
                0 : mWindowRadius;
            set => mOuterMarginSize = value;
        }

        // state true if window expand or docked
        public bool Borderless 
        { 
            get
            {
                return (mWindow.WindowState == WindowState.Maximized 
                    || mDockPosition != WindowDockPosition.Undocked);
            }
        }

        // resize border around the window
        public int ResizeBorder { get => Borderless ? 0 : resizeBorder; }

        // height of the title bar window
        public int TitleHeight { get => Borderless ? titleHeight : titleHeight - ResizeBorder; }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        // The size of the resize border around the window, taking into account the outer margin
        public Thickness ResizeBorderThickness
        {
            get
            {
                return (mWindow.WindowState == WindowState.Maximized) ?
                    new Thickness(0) : new Thickness(ResizeBorder + OuterMarginSize);
            }

        }

        // The margin around the window to allow for a drop shadow
        public Thickness OuterMarginSizeThickness { get => new Thickness(OuterMarginSize); }

        // Corner radius of the window
        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }

        public GridLength TitleHeightGridLength { get => new GridLength(TitleHeight + ResizeBorder); }

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;

        // actions for system window control buttons
        public ICommand WindowMinimizeCommand { get; set; }
        public ICommand WindowMaximizeCommand { get; set; }
        public ICommand WindowCloseCommand { get; set; }

        //public ICommand ShowWindowMenuCommand { get; set; }

        public string BodyTextBlock
        {
            get
            {
                return _CurrentOS.ToString();
            }
        }

        #endregion

        public WindowViewModel(Window window)
        {

            Debug.WriteLine(_CurrentOS.ToString());
            // this.window
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            // View model event, listen out for the window resizing
            //mWindow.StateChanged += (sender, e) =>
            //{
            //    OnPropertyChanged(nameof(Borderless));
            //    OnPropertyChanged(nameof(ResizeBorderThickness));
            //    OnPropertyChanged(nameof(OuterMarginSize));
            //    OnPropertyChanged(nameof(OuterMarginSizeThickness));
            //    OnPropertyChanged(nameof(WindowRadius));
            //    OnPropertyChanged(nameof(WindowCornerRadius));
            //    OnPropertyChanged(nameof(TitleHeightGridLength));
            //};

            // command initialization
            WindowMinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            WindowMaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            WindowCloseCommand = new RelayCommand(() => mWindow.Close());

            //ShowWindowMenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            var windowResizer = new WindowResizer(mWindow);

            // Listen out for dock changes
            windowResizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }

        #region LoginScreen
        private Messenger messenger;

        public string MainLabelText { get; set; } = "Login";

        // default values for login fields
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        // length of login fields
        public int LoginMaxLength { get; set; } = 32;
        public int PasswordMaxLength { get; set; } = 32;

        public bool DevLineVisibility { get; set; } = false;

        // view visibility for this viewmodel
        public bool LoginScreenVisibility { get; set; } = false;

        public ICommand UserAuthorizationCommand { get; set; }

        //this line should be in the constructor, if not will be issue
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

        #region Private Classes Helpers
        
        // gets the current mouse position on the screen
        private Point GetMousePosition()
        {
            // postition of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        // If the window resizes to a special position (docked or maximized)
        // this will update all required property change events to set the borders and radius values
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
            OnPropertyChanged(nameof(TitleHeightGridLength));
        }

        #endregion
    }
}
