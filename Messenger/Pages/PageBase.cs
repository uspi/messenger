using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using Messenger.Core;
using System.ComponentModel;

namespace Messenger
{
    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    public class PageBase : UserControl
    {
        #region Private Members
        private object viewModel;
        #endregion

        #region Public Properties
        // the animation the play when the page is loaded
        public PageAnimation PageLoadAnimation { get; set; } =
            PageAnimation.SlideAndFadeInFromRightAsync;

        // the animation the play when the page is unloaded
        public PageAnimation PageUnloadAnimation { get; set; } =
            PageAnimation.SlideAndFadeOutToLeftAsync;

        // the time any slide animation takes to complete
        public float SlideSeconds { get; set; } = 0.4f;

        // indicate if this page should animate out on load
        // useful for when we are moving page to another frame
        public bool ShouldAnimateOut { get; set; }

        // view model associated with this page
        public object ViewModelObject
        {
            get => viewModel;
            set
            {
                // if nothing has changed, return
                if (viewModel == value) { return; }

                viewModel = value;

                OnViewModelChanged();

                // set data context for this page
                this.DataContext = viewModel;
            }
        }
        #endregion

        #region Constructor
        public PageBase()
        {
            // don't bother animating in degin time
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            // if we are animating in, collapsed to begin with
            if (this.PageLoadAnimation != PageAnimation.None)
            {
                this.Visibility = Visibility.Collapsed;
            }

            // listen out for the page loading
            this.Loaded += PageBase_LoadedAsync;
        }
        #endregion

        #region Animations
        // Once the page is Loaded, perform any required animation
        private async void PageBase_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // don't bother animating in degin time
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            // if we are setup to animate out on load
            if (ShouldAnimateOut)
            {
                await AnimateOutAsync();
            }
            else
            {
                await AnimateInAsync();
            }
        }

        // animates the page in
        public async Task AnimateInAsync()
        {
            // make sure we have something to do
            if (this.PageLoadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (this.PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRightAsync:

                    //start the animation
                    await this.SlideAndFadeInFromRightAsync(this.SlideSeconds, 
                        width: (int)Application.Current.MainWindow.Width);
                    break;
            }
        }

        // animates the page out
        public async Task AnimateOutAsync()
        {
            // make sure we have something to do
            if (this.PageUnloadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (this.PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeftAsync:

                    //start the animation
                    await this.SlideAndFadeOutToLeftAsync(this.SlideSeconds);
                    break;
            }
        }
        #endregion

        protected virtual void OnViewModelChanged()
        {
            // override for your logic
        }
    }

    /// <summary>
    /// A base page with added ViewModel support
    /// </summary>
    public class PageBase<VM> : PageBase
        where VM : ViewModelBase, new()
    {
        #region Public Properties

        public VM ViewModel
        {
            get => (VM)ViewModelObject;
            set => ViewModelObject = value;
        }

        #endregion

        #region Constructor
        public PageBase() : base()
        {
            // create a default view model
            this.ViewModel = IoC.Get<VM>();
        }

        public PageBase(VM specificViewModel = null) : base()
        {
            if (specificViewModel != null)
            {
                ViewModel = specificViewModel;
            }
            else
            {
                // create a default view model
                this.ViewModel = IoC.Get<VM>();
            }
        }
        #endregion
    }
}
