
namespace Messenger.Core
{
    /// <summary>
    /// Options for user requests to the server
    /// </summary>
    public enum UserRequest
    {

        // if the user requested to send a message
        SendMessage = 0,

        // if the user requested to sign in
        SignIn = 1,

        // if the user requested to sign up
        SignUp = 2,

        // if user want get messages that he doesn't have
        GiveNewMessages = 3,

        // if user want create chat with another user
        CreateNewChat = 4
    }
}
