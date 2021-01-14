using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Messenger.Core
{
    /// <summary>
    /// The logic for the chat list. View model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : ViewModelBase
    {
        // chat thread items for the list
        public ObservableCollection<ChatMessageListItemViewModel> Items { get; set; }

        // chat title
        public string DisplayTitle { get; set; }

        // the text that is written in the text box
        public string PendingMessageText { get; set; }

        public ICommand SendCommand { get; set; }

        public ChatMessageListViewModel()
        {
            SendCommand = new RelayCommand(Send);
        }

        public void Send()
        {
            // if send message blank
            if (string.IsNullOrEmpty(PendingMessageText) 
                || string.IsNullOrWhiteSpace(PendingMessageText))
            {
                // clear message pending text
                PendingMessageText = null;
                return;
            }

            // if list empty, create new
            if (Items == null)
            {
                Items = new ObservableCollection<ChatMessageListItemViewModel>();
            }

            // fake send
            Items.Add(new ChatMessageListItemViewModel
            {
                ProfileInitials = "SG",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                ImAuthor = true,
                AnchorVisibility = true,
                SenderName = "Soul Goodman"
            });

            // clear pending text
            PendingMessageText = null;
        }
    }
}
