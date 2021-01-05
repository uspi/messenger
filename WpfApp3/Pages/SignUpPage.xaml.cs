
using System.Security;


namespace WPFClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class SignUpPage : BasePage<LoginViewModel>, IHavePassword
    {
        // password of this page (LoginPage.xaml)
        public SecureString SecurePassword { get => PasswordText.SecurePassword; }

        public SignUpPage()
        {
            InitializeComponent();
        }
    }
}
