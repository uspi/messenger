using System;
using System.Collections.Generic;

namespace Messenger.Core
{
    // it's for test and debug
    public class ChatMessageListDesignModel : ChatMessageListViewModel
    {
        // instance of this class
        public static ChatMessageListDesignModel Instance 
        { 
            get => new ChatMessageListDesignModel(); 
        }

        // constructor
        public ChatMessageListDesignModel() 
        {
            base.Items = new List<ChatMessageListItemViewModel>
            {
                new ChatMessageListItemViewModel
                {
                    SenderName = "Soul",
                    ProfileInitials = "SG",
                    Message = "How are you doing with my visa?",
                    ProfilePictureRGB = "28a6e0",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                    ShowProfilePicture = true,
                    AnchorVisibility = false,
                    ImAuthor = false
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Hovard",
                    ProfileInitials = "Hammlin",
                    Message = "How do you like my new tie?",
                    ProfilePictureRGB = "be15e8",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                    ShowProfilePicture = false,
                    AnchorVisibility = true,
                    ImAuthor = true
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Soul",
                    ProfileInitials = "SG",
                    Message = "Just answer the question, I pay you money!",
                    ProfilePictureRGB = "28a6e0",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                    ShowProfilePicture = false,
                    AnchorVisibility = true,
                    ImAuthor = false
                },
            };
        }
    }
}
