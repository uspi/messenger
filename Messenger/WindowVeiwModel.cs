using System.Windows;
using System.Windows.Input;
using Messenger.Core;

namespace Messenger
{
    public class WindowViewModel : ViewModelBase
    {
        #region Private Members
        // Window this view model controls
        private Window window;

        // the margin around the window to allow for a drop shadow
        private int mOuterMarginSize = 0;//10

        // Corner radius of the window
        private int windowRadius = 0;

        private int resizeBorder = 6;

        private int titleHeight = 22;

        private WindowDockPosition mDockPosition { get; set; } = WindowDockPosition.Undocked;
        #endregion

        #region Public Properties

        public double WindowMinimumWidth { get; set; } = 450;

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
                (window.WindowState == WindowState.Maximized) ?
                0 : windowRadius;
            set => mOuterMarginSize = value;
        }

        // state true if window expand or docked
        public bool Borderless 
        { 
            get
            {
                return (window.WindowState == WindowState.Maximized 
                    || mDockPosition != WindowDockPosition.Undocked);
            }
        }

        // resize border around the window
        public int ResizeBorder { get => Borderless ? 0 : resizeBorder; }

        // height of the title bar window
        public int TitleHeight { get => Borderless ? 
                titleHeight : titleHeight - ResizeBorder; }

        // border so that the window does not merge with others program windows
        public Thickness ContentBorderThickness 
        { 
            get => Borderless ? 
                new Thickness(0) : new Thickness(0.1, 0, 0.1, 0.1);
        }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        // the size of the resize border around the window, 
        // taking into account the outer margin
        public Thickness ResizeBorderThickness
        {
            get
            {
                return Borderless ?
                    new Thickness(0) : new Thickness(ResizeBorder + OuterMarginSize);
            }

        }

        // The margin around the window to allow for a drop shadow
        public Thickness OuterMarginSizeThickness { get => new Thickness(OuterMarginSize); }

        // Corner radius of the window
        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }

        public GridLength TitleHeightGridLength { get => new GridLength(TitleHeight + ResizeBorder); }
        #endregion

        #region Commands

        // actions for system window control buttons
        public ICommand WindowMinimizeCommand { get; set; }
        public ICommand WindowMaximizeCommand { get; set; }
        public ICommand WindowCloseCommand { get; set; }
        //public ICommand ShowWindowMenuCommand { get; set; }
        #endregion

        #region Constructor
        // passing in parameter Syste.Windows.Window, you break MVVM
        // but here it is justified because we completely redo the window
        public WindowViewModel(Window _window)
        {
            this.window = _window;

            // Listen out for the window resizing
            window.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            // command initialization
            WindowMinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            WindowMaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized);
            WindowCloseCommand = new RelayCommand(() => window.Close());
            //ShowWindowMenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(window, GetMousePosition()));

            // now the taskbar does not overlap the program window =>
            var windowResizer = new WindowResizer(this.window);

            // Listen out for dock changes
            windowResizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }
        #endregion 

        #region Private Helpers

        // gets the current mouse position on the screen
        private Point GetMousePosition()
        {
            // postition of the mouse relative to the window
            var position = Mouse.GetPosition(window);

            // add the window position so its a "ToScreen"
            return new Point(position.X + window.Left, position.Y + window.Top);
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
            OnPropertyChanged(nameof(ContentBorderThickness));
        }

        #endregion
    }
}
