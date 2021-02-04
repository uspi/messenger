using System;

namespace Messenger.Core
{
    /// <summary>
    /// A view model for each message thread item 
    /// in a chat thread
    /// </summary>
    public class ChatMessageListItemViewModel : ViewModelBase, ICloneable
    {
        // name of the message sender
        public string SenderName { get; set; }

        // latest message displayed in this item
        public string Message { get; set; }

        // initials default showing in the profile picture
        public string ProfileInitials { get; set; }

        // profile picture background RGB color in hex
        public string ProfilePictureRGB { get; set; }

        #region Date And Time

        // when the message was read
        // Default: DateTimeOffset.MinValue (unread message)
        public DateTimeOffset MessageReadTime { get; set; }

        // when the message was sent
        // Default: DateTimeOffset.MinValue (unsent message)
        public DateTimeOffset MessageSentTime { get; set; } 

        #endregion

        #region Flags

        // true if message has been read
        public bool MessageRead { get => MessageReadTime > DateTimeOffset.MinValue; }

        // if user click on this item - true
        public bool IsSelected { get; set; }

        // whether to display a photo of the contact who owns this message
        public bool ShowProfilePicture { get; set; }

        // display the anchor(tail) of the message
        public bool AnchorVisibility
        {
            get;// to do: condition if message contains not text or in group messages
            set;
        }

        // true if this message was written by me, (current user). 
        // needed to align messages to the right
        public bool ImAuthor { get; set; } 

        // true if we have message
        public bool HasMessage { get => Message != null; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
