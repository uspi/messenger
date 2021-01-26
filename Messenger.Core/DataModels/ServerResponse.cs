
namespace Messenger.Core
{
    /// <summary>
    /// Options for server response to the client
    /// </summary>
    public enum ServerResponse
    {
        // if user input wrong sign in information
        SignInFailed = 0,

        // if user input wrong sign up information
        SignUpFailed = 1,

        // if the login is successful, I send chats and messages of this user
        SendAllInformation = 2,

        // sending new messages for chats
        SendNewMessages = 3,

        // if we have error like answer on response 
        Error = 4
    }
}
