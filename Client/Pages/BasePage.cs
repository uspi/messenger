using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;

namespace WPFClient
{
    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    public class BasePage<VM> : Page 
        where VM : BaseViewModel, new()
    {
        #region Private Members
        private VM viewModel;
        #endregion

        #region Public Properties

        // the animation the play when the page is loaded
        public PageAnimation PageLoadAnimation { get; set; } = 
            PageAnimation.SlideAndFadeInFromRight;

        // the animation the play when the page is unloaded
        public PageAnimation PageUnloadAnimation { get; set; } = 
            PageAnimation.SlideAndFadeOutToLeft;

        // the time any slide animation takes to complete
        public float SlideSeconds { get; set; } = 0.8f;

        // view model associated with this page
        public VM ViewModel 
        { 
            get => viewModel;
            set
            {
                // if nothing has changed, return
                if (viewModel == value) { return; }

                viewModel = value;

                // set data context for this page
                this.DataContext = viewModel;
            }
        }
        #endregion

        #region Constructor
        public BasePage()
        {
            // if we are animating in, collapsed to begin with
            if (this.PageLoadAnimation != PageAnimation.None)
            {
                this.Visibility = Visibility.Collapsed;
            }

            // listen out for the page loading
            this.Loaded += BasePage_Loaded;

            // create a default view model
            this.ViewModel = new VM();
        }
        #endregion

        #region Animations
        // Once the page is Loaded, perform any required animation
        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        // animates the page in
        public async Task AnimateIn()
        {
            // make sure we have something to do
            if (this.PageLoadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (this.PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:

                    //start the animation
                    await this.SlideAndFadeInFromRight(this.SlideSeconds);
                    break;
            }
        }

        // animates the page out
        public async Task AnimateOut()
        {
            // make sure we have something to do
            if (this.PageUnloadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (this.PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:

                    //start the animation
                    await this.SlideAndFadeOutToLeft(this.SlideSeconds);
                    break;
            }
        }
        #endregion
    }
}
