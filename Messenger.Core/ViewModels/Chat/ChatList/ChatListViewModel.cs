using System.Collections.Generic;

namespace Messenger.Core
{
    /// <summary>
    /// The logic for the chat list. The unit of 
    /// content is <see cref="ChatListItemViewModel"/>
    /// </summary>
    public class ChatListViewModel : ViewModelBase
    {
        public List<ChatListItemViewModel> Items { get; set; }

        public ChatListViewModel()
        {
            Items = new List<ChatListItemViewModel>();
        }

        // TODO: methods for adding in Items New position
    }
}
