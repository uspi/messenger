namespace WPFClient
{
    public class ChatListItemViewModel : BaseViewModel
    {
        // name displayed in chat list 
        public string Name { get; set; }

        // latest message displayed in this item
        public string Message { get; set; }

        // initials default showing in the profile picture
        public string ProfileInitials { get; set; }

        // picture of this chat item profile
        public string ProfilePictureRGB { get; set; }
    }
}
