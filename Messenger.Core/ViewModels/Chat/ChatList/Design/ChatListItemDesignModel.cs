
namespace Messenger.Core
{
    // it's for test and debug
    public class ChatListItemDesignModel : ChatListItemViewModel
    {
        // instance of this class
        public static ChatListItemDesignModel Instance { get => new ChatListItemDesignModel(); }

        // constructor
        public ChatListItemDesignModel() 
        {
            ProfileInitials = "SG";
            Name = "Saul Goodman";
            Message = "Can you do something for me? The best " +
                "teachers are those who show you where to look, but don't tell you what to see";
            ProfilePictureRGB = "009be3";
        }
    }
}
