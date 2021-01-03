using System.Security;
using System.Windows.Controls;

namespace WPFClient
{
    /// <summary>
    /// Creates an interface for the ViewModel.cs to access 
    /// the View.xaml <see cref="PasswordBox"/> field value
    /// </summary>
    interface IHavePassword
    {
        // content the password box
        SecureString SecurePassword { get; }
    }
}
