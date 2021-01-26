using Messenger.Core;
using System;
using System.Collections.Generic;

namespace Messenger.Core
{
    /// <summary>
    /// Class that are implemented for communication between server and client. 
    /// Client Request to server
    /// </summary>
    public class Request : EventArgs
    {
        // type by which the server determines how to process this request
        public UserRequest UserRequestType { get; set; }

        // the sequence number of this request
        public long RequestId { get; set; }

        // request creation time
        public DateTimeOffset CreationMoment { get; set; }

        // data of the user initiating the request (id, creation time etc.)
        public User UserInitiator { get; set; }

        // message that the requestor wants to send
        public Message Message { get; set; }

        // chat that the requestor wants to send
        public Chat TargetChat { get; set; }

        // the chats that the user has, the server needs to send 
        // only the missing chats
        public IList<Chat> ExistingChats { get; set; }

        // the messages that the user has, the server needs to send 
        // only the missing messages
        public IList<Message> ExistingMessages { get; set; }

        public string UserNickNameWantToFind { get; set; }
    }

    /// <summary>
    /// Class that are implemented for communication between server and client. 
    /// Server Response to client
    /// </summary>
    public class Response : EventArgs
    {
        // type by which the user determines how to process this response
        public ServerResponse ServerResponseType { get; set; }

        // the sequence number of this answer
        public long ResponseId { get; set; }

        // response creation time
        public DateTimeOffset CreationMoment { get; set; }

        // all user info (id, creation time etc.)
        public User UserInfo { get; set; }

        // sent only at login, contains all the parts of this user. 
        // in other cases equal null
        public IList<Chat> Chats { get; set; }

        // sent only at login, contains all the parts of this user. 
        // in other cases equal null
        public IList<Message> Messages { get; set; }

        // channel for sending new messages for the user, 
        // when authorization is equal to
        public IList<Message> NewMessages { get; set; }

        // if resoponse type error send this text
        public string ErrorMessage { get; set; }
    }
}
