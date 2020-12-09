using PropertyChanged;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Security;
using System.Windows.Shapes;

namespace WPFClient
{
    [AddINotifyPropertyChangedInterface]
    public class WindowViewModel : Notifier
    {
        internal CurrentOS _CurrentOS { get; } = new CurrentOS();

        // os cases todo
        #region operating system dependent properties

        public bool AllowsTransparency { get => true; }

        // Kludge у window.chrome есть косяк(на англ. kludge) когда окно на весь экран по краям окна съедается 
        // несколько пикселей, чтобы это компенсировать нужна эта переменная
        public Thickness WindowBodyContentPadding
        {
            get
            {
                if (mWindow.WindowState == WindowState.Maximized)
                {
                    if (_CurrentOS.Version > 6.3)
                    {
                        return new Thickness(6, 0, 6, 6);
                    }
                    else
                        return new Thickness(3, 0, 3, 3);
                }
                else
                    return new Thickness(0);
            }
        }

        public Thickness WindowBodyTitlePadding
        {
            get
            {
                if (mWindow.WindowState == WindowState.Maximized)
                {
                    // more than windows 8.1 == v6.3
                    if (_CurrentOS.Version > 6.3)
                    {
                        return new Thickness(0, 6, 6, 0);
                    }
                    else
                        return new Thickness(0, 3, 3, 0);
                }
                else 
                    return new Thickness(0);
            }       
        }

        #endregion

        // Window this view model controls
        private Window mWindow;

        // The margin around the window to allow for a drop shadow
        // размер поля с тень
        private int mOuterMarginSize = 0;

        // Corner radius of the window
        private int mWindowRadius = 0;

        #region Public Properties

        public Thickness GlassFrameThickness
        {
            get => new Thickness(0);
        }

        // resize border around the window
        public int ResizeBorder { get; set; } = 6;

        // The size of the resize border around the window, taking into account the outer margin
        public Thickness ResizeBorderThickness 
        { 
            get => 
                (mWindow.WindowState == WindowState.Maximized) ? 
                new Thickness(0) : new Thickness(ResizeBorder * 2); 
        }

        // The margin around the window to allow for a drop shadow
        public int OuterMarginSize
        {
            get => 
                (mWindow.WindowState == WindowState.Maximized) ? 
                0 : mOuterMarginSize;
            set => mOuterMarginSize = value;
        }

        // The margin around the window to allow for a drop shadow
        public Thickness OuterMarginSizeThickness { get => new Thickness(OuterMarginSize); }

        // Corner radius of the window
        public int WindowRadius
        {
            get => 
                (mWindow.WindowState == WindowState.Maximized) ? 
                0 : mWindowRadius;
        }

        // Corner radius of the window
        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }

        // height of the title bar window
        public int TitleHeight { get; set; } = 22;

        public GridLength TitleHeightGridLength 
        { 
            get => (mWindow.WindowState == WindowState.Maximized) ? 
                new GridLength(TitleHeight + ResizeBorder) : new GridLength(TitleHeight); 
        }

        // actions for system window control buttons
        public ICommand WindowMinimizeCommand { get; set; }
        public ICommand WindowMaximizeCommand { get; set; }
        public ICommand WindowCloseCommand { get; set; }

        //public ICommand ShowWindowMenuCommand { get; set; }

        public string BodyTextBlock
        {
            get
            {
                return _CurrentOS.ToString() + 
                    "\nAllowsTransparency: " + AllowsTransparency +
                    "\nGlassFrameThickness.Top: " + GlassFrameThickness.Top;
            }
        }

        #endregion

        public WindowViewModel(Window window)
        {
            float a = _CurrentOS.Version;
            string systemOSInfo = _CurrentOS.ToString();

            // this.window
            mWindow = window;

            //Rect a = SystemParameters.WorkArea;
            //double workAreaHeight = a.Height;
            //double workAreaWidth = a.Width;

            // View model event, listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(TitleHeightGridLength));
                OnPropertyChanged(nameof(WindowBodyContentPadding));
                OnPropertyChanged(nameof(WindowBodyTitlePadding));
            };

            // command initialization
            WindowMinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            WindowMaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            WindowCloseCommand = new RelayCommand(() => mWindow.Close());
            
            //ShowWindowMenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));
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

        

        #endregion
    }
}
