namespace Messenger.Core
{
    public class ApplicationViewModel : ViewModelBase
    {
        // start page for the application
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.SignIn;

        public bool SideMenuVisibility { get; set; } = false;
    }
}
