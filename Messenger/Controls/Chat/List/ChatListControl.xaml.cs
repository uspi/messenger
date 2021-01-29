using System.Windows.Controls;
using Messenger.Core;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for ChatListControl.xaml
    /// </summary>
    public partial class ChatListControl : UserControl
    {
        public ChatListControl()
        {
            InitializeComponent();
            this.DataContext = IoC.Get<ChatListViewModel>();
        }
    }
}
