using System.Security;

namespace Messenger.Core
{
    /// <summary>
    /// Creates an interface for the ViewModel.cs to access 
    /// the View.xaml "PasswordBox" field value
    /// </summary>
    public interface IHavePassword
    {
        // content the password box
        SecureString SecurePassword { get; }
    }
}
