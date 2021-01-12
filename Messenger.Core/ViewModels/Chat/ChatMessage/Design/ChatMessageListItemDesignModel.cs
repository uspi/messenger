using System;

namespace Messenger.Core
{
    // it's for test and debug
    public class ChatMessageListItemDesignModel : ChatMessageListItemViewModel
    {
        // instance of this class
        public static ChatMessageListItemDesignModel Instance 
        { 
            get => new ChatMessageListItemDesignModel(); 
        }

        // constructor
        public ChatMessageListItemDesignModel() 
        {
            SenderName = "Soul";
            ProfileInitials = "SG";
            Message = "Just answer the question, I pay you money! (info from: ChatMessageListItemDesignModel)";
            ProfilePictureRGB = "28a6e0";
            MessageSentTime = DateTimeOffset.UtcNow;
            MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3));
            ShowProfilePicture = false;
            AnchorVisibility = true;
            ImAuthor = false;
        }
    }
}
