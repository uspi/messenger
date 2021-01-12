using System.Collections.Generic;

namespace Messenger.Core
{
    /// <summary>
    /// The logic for the chat list. View model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : ViewModelBase
    {
        // chat thread items for the list
        public List<ChatMessageListItemViewModel> Items { get; set; }
    }
}
