using System;

namespace Messenger.Core
{
    public class ApplicationViewModel : ViewModelBase
    {
        // start page for the application
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.SignIn;

        public bool SideMenuVisibility { get; set; } = false;

        // navigates to the specified page
        public void GoToPage(ApplicationPage page)
        {
            // set needed page 
            CurrentPage = page;

            // rule of when to show side menu
            SideMenuVisibility = page == ApplicationPage.Chat;
        }
    }
}
