using System.Security;
using Messenger.Core;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class SignUpPage : BasePage<SignInViewModel>, IHavePassword
    {
        // password of this page (LoginPage.xaml)
        public SecureString SecurePassword { get => PasswordText.SecurePassword; }

        public SignUpPage()
        {
            InitializeComponent();
        }
    }
}
