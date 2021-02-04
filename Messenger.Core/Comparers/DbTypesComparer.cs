using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger.Core
{
    /// <summary>
    /// Checks equality based on the number of chat messages
    /// </summary>
    public class ChatCountComparer : IEqualityComparer<Chat>
    {
        public bool Equals(Chat x, Chat y)
        {
            // if count of messages in chat x equals 
            // cout of messages in chat y
            if (x.Messages.Count == y.Messages.Count)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Chat obj)
        {
            // get hash code for creation time property
            int hashCreationTime = obj.CreatedAt.GetHashCode();

            // get hash code for text property
            int hashId = obj.Id.GetHashCode();

            // calculate hash coed
            return hashCreationTime ^ hashId;
        }
    }

    /// <summary>
    /// Checks equality based on the id. If message 
    /// ids are equal, messages are equal
    /// </summary>
    public class MessageIdComparer : IEqualityComparer<Message>
    {
        public bool Equals(Message x, Message y)
        {
            // if message ids are equal, messages are equal
            if (x.Id == y.Id)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Message obj)
        {
            // get hash code for creation time property
            int hashCreationTime = obj.CreatedAt.GetHashCode();

            // get hash code for text property
            int hashText = obj.Text.GetHashCode();

            // calculate hash coed
            return hashCreationTime ^ hashText;
        }
    }
}
