using System.Collections.Generic;

namespace WPFClient
{
    /// <summary>
    /// The logic for the chat list. The unit of 
    /// content is <see cref="ChatListItemViewModel"/>
    /// </summary>
    public class ChatListViewModel : BaseViewModel
    {
        public List<ChatListItemViewModel> Items { get; set; }
    }
}
