using Messenger.Core;
using System.Security;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class SignInPage : PageBase<SignInViewModel>, IHavePassword
    {
        // password of this page (LoginPage.xaml)
        public SecureString SecurePassword { get => PasswordText.SecurePassword; }
        
        public SignInPage()
        {
            InitializeComponent();
        }

        public SignInPage(SignInViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
