using System;
using System.Linq;
using System.Windows.Input;

namespace Messenger.Core
{
    /// <summary>
    /// A unit of the chat list, may contain, for example, 
    /// a dialog with the author's <see cref="Name"/>, 
    /// his <see cref="ProfileInitials"/> 
    /// in the picture, the last message and the <see cref="ProfilePictureRGB"/>
    /// </summary>
    public class ChatListItemViewModel : ViewModelBase, ICloneable
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

        // list of messages
        public ChatMessageListViewModel CurrentChatMessageList { get; set; } 
            = new ChatMessageListViewModel();

        public ChatListItemViewModel()
        {
            // set command for opening this dialog
            OpenMessageCommand = new RelayCommand(OpenMessage);

            // create message list of item for this item dialog
            CurrentChatMessageList = new ChatMessageListViewModel();
        }

        public void OpenMessage()
        {
            // open a dialog and place messages in it
            IoC.Application.GoToPage(ApplicationPage.Chat, CurrentChatMessageList);


            // the user is now in this chat
            IsSelected = true;

            var itemsFromChatViewModel = IoC.Get<ChatListViewModel>().Items;


            // all selected chats
            var allSelectedChats = itemsFromChatViewModel.Where(i => i.IsSelected).ToList();

            // all other chats except this
            var otherChats = allSelectedChats
                .Where(c => c.CurrentChat.Id != this.CurrentChat.Id)
                .ToList();

            // this chat
            var thisChat = itemsFromChatViewModel
                .Where(c => c.CurrentChat.Id == this.CurrentChat.Id)
                .FirstOrDefault();

            // set other items select to true
            IoC.Get<ChatListViewModel>().Items.Where(i => i.CurrentChat.Id == thisChat.CurrentChat.Id).FirstOrDefault().IsSelected = true;

            // remove flags from all other chats except this one
            foreach (var otherChat in otherChats)
            {
                // set other items select to false
                IoC.Get<ChatListViewModel>().Items.Where(i => i.CurrentChat.Id == otherChat.CurrentChat.Id).FirstOrDefault().IsSelected = false;
            }
        }

        public object Clone()
        {
            return
                new ChatListItemViewModel
                {
                    NewMessageAvailable = this.NewMessageAvailable,
                    IsSelected = this.IsSelected,
                    Name = this.Name,
                    Message = this.Message,
                    ProfileInitials = this.ProfileInitials,
                    ProfilePictureRGB = this.ProfilePictureRGB,
                    OpenMessageCommand = this.OpenMessageCommand,
                    CurrentChat = (Chat)this.CurrentChat.Clone(),
                    CurrentChatMessageList = 
                        (ChatMessageListViewModel)this.CurrentChatMessageList.Clone()
                }; 
        }
    }
}
