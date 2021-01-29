using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Messenger.Core
{
    /// <summary>
    /// A unit of the chat list, may contain, for example, 
    /// a dialog with the author's <see cref="Name"/>, 
    /// his <see cref="ProfileInitials"/> 
    /// in the picture, the last message and the <see cref="ProfilePictureRGB"/>
    /// </summary>
    public class ChatListItemViewModel : ViewModelBase
    {
        // indicator of new messages
        public bool NewMessageAvailable { get; set; }

        // if user click on this item - true
        public bool IsSelected { get; set; }

        // name displayed in chat list 
        public string Name { get; set; }

        // latest message displayed in this item
        public string Message { get; set; }

        // initials default showing in the profile picture
        public string ProfileInitials { get; set; }

        // profile picture background RGB color in hex
        public string ProfilePictureRGB { get; set; } 

        // open message thread
        public ICommand OpenMessageCommand { get; set; }

        // entity of current chat
        public Chat CurrentChat { get; set; }

        public ChatMessageListViewModel CurrentChatMessageList { get; set; } 
            = new ChatMessageListViewModel();

        public ChatListItemViewModel()
        {
            // set command for opening this dialog
            OpenMessageCommand = new RelayCommand(OpenMessage);

            // create message list of item for this item dialog
            CurrentChatMessageList = new ChatMessageListViewModel();
        }

        private void OpenMessage()
        {
            // open a dialog and place messages in it
            IoC.Application.GoToPage(ApplicationPage.Chat, CurrentChatMessageList);
        }
    }
}
