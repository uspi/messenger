using System;
using System.Collections;
using System.Collections.Generic;

namespace Messenger.Core
{
    /// <summary>
    /// Data that must be transferred from the user to the <see cref="Server"/> 
    /// to fulfill the <see cref="UserRequest.SendMessage"/>
    /// </summary>
    public class SendMessageRequestEventArgs : EventArgs
    {
        public long ToChatId { get; set; }

        public long FromUserId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Text { get; set; }
    }

    /// <summary>
    /// Data that must be transferred from the user to the <see cref="Server"/> 
    /// to fulfill the <see cref="UserRequest.SignIn"/>
    /// </summary>
    public class SignInRequestEventArgs : EventArgs
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    /// <summary>
    /// Data that must be transferred from the user to the <see cref="Server"/> 
    /// to fulfill the <see cref="UserRequest.SignUp"/>
    /// </summary>
    public class SignUpRequestEventArgs : EventArgs
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Nick { get; set; }
    }

    /// <summary>
    /// Data that must be transferred from the user to the <see cref="Server"/> 
    /// to fulfill the <see cref="UserRequest.CreateNewChat"/>
    /// </summary>
    public class CreateNewChatRequestEventArgs : EventArgs
    {
        // authomatic member in this new chat
        public string ChatPartnerNickname { get; set; }

        public long OwnerId { get; set; }

        public string OwnerNick { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool IsChannel { get; set; }
    }

    /// <summary>
    /// Event for userEnvironment which checks the server database 
    /// and if new messages appear it sends to the client 
    /// </summary>
    public class NoticedNewMessage : EventArgs
    {
        public Message NoticedNewMessageInstace { get; set; }
    }

    /// <summary>
    /// Event for userEnvironment which checks the server database 
    /// and if new messages appear it sends to the client 
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Data that must be transferred from the user to the <see cref="Server"/> 
    /// to fulfill the <see cref="UserRequest.GiveNewMessages"/>
    /// </summary>
    //public class GiveNewMessagesEventArgs : EventArgs
    //{
    //    // we tell the server what we already have so that 
    //    // it does not send the data that we already have
    //    public IList<Message> AllMyCurrentMessages { get; set; }
    //}
}
