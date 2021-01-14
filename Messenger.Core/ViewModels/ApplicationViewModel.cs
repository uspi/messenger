using System;

namespace Messenger.Core
{
    public class ApplicationViewModel : ViewModelBase
    {
        // start page for the application
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Chat;

        public ViewModelBase CurrentPageViewModel { get; set; }

        public bool SideMenuVisibility { get; set; } = true;

        // navigates to the specified page
        public void GoToPage(ApplicationPage page, ViewModelBase viewModel = null)
        {
            CurrentPageViewModel = viewModel;

            // set needed page 
            CurrentPage = page;

            OnPropertyChanged(nameof(CurrentPage));

            // rule of when to show side menu
            SideMenuVisibility = page == ApplicationPage.Chat;
        }
    }
}
