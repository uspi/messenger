using Messenger.Core;
using System;
using System.ComponentModel;
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
        public ApplicationPage CurrentPage
        {
            get { return (ApplicationPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Registers CurrentPage as a dependency property
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                name: nameof(CurrentPage),
                propertyType: typeof(ApplicationPage),
                ownerType: typeof(PageHost),
                typeMetadata: new UIPropertyMetadata(
                    defaultValue: default(ApplicationPage), 
                    propertyChangedCallback: null, 
                    coerceValueCallback: CurrentPagePropertyChanged));

        // current page to show in the page host
        public ViewModelBase CurrentPageViewModel
        {
            get { return (ViewModelBase)GetValue(CurrentPageViewModelProperty); }
            set { SetValue(CurrentPageViewModelProperty, value); }
        }

        // Registers CurrentPage as a dependency property
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(
                name: nameof(CurrentPageViewModel),
                propertyType: typeof(ViewModelBase),
                ownerType: typeof(PageHost),
                typeMetadata: new UIPropertyMetadata());

        #endregion

        #region Property Chaged Events
        // called when the CurrentPage value has changed
        private static object CurrentPagePropertyChanged(
            DependencyObject d, object value)
        {
            // current values
            var currentPage = (ApplicationPage)d.GetValue(CurrentPageProperty);
            var currentPageViewmodel = d.GetValue(CurrentPageViewModelProperty);

            // get frames
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame= (d as PageHost).OldPage;

            // if the type of the current page has not changed, 
            // change only the view model
            if (newPageFrame.Content is PageBase page &&
                page.ToApplicationPage() == currentPage)
            {
                // update view model
                page.ViewModelObject = currentPageViewmodel;

                return value;
            }

            // store current page content as the previous page
            var oldPageContent = newPageFrame.Content;

            // remove current page from next page frame
            newPageFrame.Content = null;

            // move previous page into old page frame
            oldPageFrame.Content = oldPageContent;
             
            // animate out previous page when the loaded events fires
            // right after this call due to moving frames
            if (oldPageContent is PageBase oldPage)
            {
                // tell previous page animate out
                oldPage.ShouldAnimateOut = true;

                // when previous page animation done, remove old page
                Task.Delay((int)(oldPage.SlideSeconds * 1000))
                    .ContinueWith((t) =>
                {
                    // in ui thread
                    Application.Current.Dispatcher.Invoke(
                        () => oldPageFrame.Content = null);
                    
                });
                

                // We don't want to wait for the out animation to finish, 
                // before start animating the next frames entrance. So we don't await
                _ = Task.Run(oldPage.AnimateOutAsync);
            }

            // set new page content
            newPageFrame.Content = currentPage.Convert(currentPageViewmodel);

            return value;
        } 
        #endregion

        #region Constructor
        public PageHost()
        {
            InitializeComponent();

            // show current page as the dependency prop does not fire
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.NewPage.Content = IoC.Application.CurrentPage.Convert();
            }
        } 
        #endregion
    }
}
