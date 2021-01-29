using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Messenger.Core
{
    /// <summary>
    /// The logic for the chat list. The unit of 
    /// content is <see cref="ChatListItemViewModel"/>
    /// </summary>
    public class ChatListViewModel : ViewModelBase
    {
        // all user chats
        public List<ChatListItemViewModel> Items { get; set; } = new List<ChatListItemViewModel>();

        // constructor
        public ChatListViewModel() { }
    }
}
