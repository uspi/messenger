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
    }
}
