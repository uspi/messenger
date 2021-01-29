﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Messenger.Core
{
    // for convenient transmission of a message with all the details to the server
    public delegate void NewChatMessageHandler(ChatMessageListItemViewModel message);

    /// <summary>
    /// The logic for the chat list. View model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : ViewModelBase
    {
        // event if we sended new message in dialog
        public event NewChatMessageHandler NewChatMessage;

        // chat thread items for the list
        public ObservableCollection<ChatMessageListItemViewModel> Items { get; set; }

        // chat title
        public string DisplayTitle { get; set; }

        // name of current user of this chat, 
        // needed for sending new messages
        public string AuthorName { get; set; }

        // initials of current user of this chat, 
        // needed for sending new messages
        public string AuthorProfileInitials { get; set; }

        // the text that is written in the text box
        public string PendingMessageText { get; set; }

        public ICommand SendCommand { get; set; }

        // needed to show that this view model should not work, messages cannot be sent from it
        public bool IsBootScreenStub { get; set; }

        public ChatMessageListViewModel()
        {
            
            SendCommand = new RelayCommand(Send);
        }

        public void Send()
        {
            // if this view model is fake
            if (IsBootScreenStub)
            {
                return;
            }

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

            // create a message template for sending
            var newMessage = new ChatMessageListItemViewModel
            {
                ProfileInitials = AuthorProfileInitials,
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                ImAuthor = true,
                AnchorVisibility = true,
                SenderName = AuthorName
            };

            // notify the system that a new message has been 
            // created and send this message in a notification
            NewChatMessage(newMessage);

            // fake send
            // add in collection this message, needed 
            // for visual display at the client
            Items.Add(newMessage);

            // clear pending text
            PendingMessageText = null;
        }
    }
}
