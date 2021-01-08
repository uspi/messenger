using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        #region Dependency Properties
        // current page to show in the page host
        public PageBase CurrentPage
        {
            get { return (PageBase)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Registers CurrentPage as a dependency property
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                name: nameof(CurrentPage),
                propertyType: typeof(PageBase),
                ownerType: typeof(PageHost),
                typeMetadata: new UIPropertyMetadata(CurrentPagePropertyChanged));

        #endregion
        
        #region Property Chaged Events
        // called when the CurrentPage value has changed
        private static void CurrentPagePropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // get frames
            Frame nextPageFrame = (d as PageHost).NextPage;
            Frame previousPageFrame= (d as PageHost).PreviousPage;

            // store current page content as the previous page
            var previousPageContent = nextPageFrame.Content;

            // remove current page from next page frame
            nextPageFrame.Content = null;

            // move previous page into old page frame
            previousPageFrame.Content = previousPageContent;
             
            // animate out previous page when the loaded events fires
            // right after this call due to moving frames
            if (previousPageContent is PageBase previousPage)
            {
                previousPage.ShouldAnimateOut = true;


                // We don't want to wait for the out animation to finish, 
                // before start animating the next frames entrance. So we don't await
                //_ = Task.Run(previousPage.AnimateOutAsync);
            }

            // set nextPage content
            nextPageFrame.Content = e.NewValue;
        } 
        #endregion

        #region Constructor
        public PageHost()
        {
            InitializeComponent();
        } 
        #endregion
    }
}
