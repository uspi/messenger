
namespace Messenger.Core
{
    public static class UserRequestTemplates
    {
        // form a ready request for sending new message
        public static Request UserSendNewMessageRequest(
            long targetChatId, string targetText, long senderUserId)
        {
            return new Request
            {
                UserRequestType = UserRequest.SendMessage,
                UserInitiator = new User { Id = (int)senderUserId /* todo: remove this cast to int */ },
                TargetChat = new Chat { Id = targetChatId },
                Message = new Message { Text = targetText }
            };
        }

        // form a ready request for sign up
        public static Request UserSignUpRequest(
            string login, string password, string nick = "Default_Nick")
        {
            return new Request
            {
                UserRequestType = UserRequest.SignUp,
                UserInitiator =
                    new User
                    {
                        Email = login,
                        Password = password,
                        Nick = nick
                    }
            };
        }

        // form a ready request for sign in
        public static Request UserSignInRequest(
            string login, string password)
        {
            return new Request
            {
                UserRequestType = UserRequest.SignIn,
                UserInitiator =
                    new User
                    {
                        Email = login,
                        Password = password,
                    }
            };
        }

        // the user creates a new chat and adds a user with a nickname to it
        public static Request UserCreatedNewChatRequest(
            string chatPartnerNickname, long chatOwnerId,
            string chatOwnerNick, bool isChannel = false)
        {
            return new Request
            {
                UserRequestType = UserRequest.CreateNewChat,
                UserInitiator =
                    new User
                    {
                        // todo: remove this cast
                        Id = (int)chatOwnerId,
                        Nick = chatOwnerNick
                    },
                UserNickNameWantToFind = chatPartnerNickname,
                TargetChat =
                    new Chat
                    {
                        IsChannel = false
                    }
            };
        }
    }
}
