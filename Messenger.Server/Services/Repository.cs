using Messenger.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Server
{
    /// <summary>
    /// Repository for data access. Contains methods for getting, 
    /// adding and modifying data.
    /// </summary>
    public class Repository : IDisposable
    {
        #region Private Fields

        private DataBaseContext _ctx; 

        #endregion

        #region Constructor

        public Repository(DataBaseContext context)
        {
            this._ctx = context;

            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            SeedData.SeedDatabase(this._ctx);
        }

        #endregion

        #region Messages

        public List<Message> GetChatMessages(Chat chat)
        {
            return _ctx.Messages
                .Where(m => m.ChatId == chat.Id)
                .ToList();
        }

        public void AddMessage(Message message)
        {
            this._ctx.Messages.Add(message);
        }

        #endregion

        #region Users

        public User GetUserByLoginAndPassword(string login, string password)
        {
            if (login == null || password == null)
            {
                return null;
            }

            return this._ctx.Users
                .Where(c => c.Email == login && c.Password == password)
                .FirstOrDefault();
        }

        public User GetChatMember(Chat chat)
        {
            return _ctx.Chats
                .Where(m => m.Id == chat.Id)
                .Select(r => r.MemberUser)
                .FirstOrDefault();
        }

        public User GetChatOwner(Chat chat)
        {
            return _ctx.Chats
                .Where(m => m.Id == chat.Id)
                .Select(r => r.OwnerUser)
                .FirstOrDefault();
        }

        /// <param name="id"></param>
        /// <returns>Returns <see cref="User"/>. If not found, returns <see cref="null"/></returns>
        public User GetUserById(long id)
        {
            return this._ctx.Users
                .Where(c => c.Id == id)
                .FirstOrDefault();
        }

        #endregion

        #region Chats

        public Chat GetChatById(long id)
        {
            return this._ctx.Chats
                .Where((c) => c.Id == id)
                .FirstOrDefault();
        }

        public List<Chat> GetAllUserOwnedChats(User user)
        {
            return this._ctx.Chats
                .Where(c => c.OwnerUser.Id == user.Id)
                .ToList();
        }

        public List<Chat> GetAllUserMemberChats(User user)
        {
            return this._ctx.Chats
                .Where(c => c.MemberUser.Id == user.Id)
                .ToList();
        }

        public List<Chat> GetUserChats(User user)
        {
            return this._ctx.Chats
                .Where(c => c.MemberUser.Id == user.Id
                         || c.OwnerUser.Id == user.Id)
                .ToList();
        }

        /// <summary>
        /// Installed property <see cref="Chat.Owner"/> to <see cref="null"/> 
        /// in all chats in which the user is a owner.
        /// May be necessary for deletion from a database 
        /// that is not configured for cascading delete
        /// </summary>
        /// <param name="user">User you want to replace with null</param>
        /// <returns></returns>
        public bool SetOwnerPropertyToNullInChats(User user)
        {
            try
            {
                // find all chat where user owner
                var allOwnedChats = this.GetAllUserOwnedChats(user);

                // set all owned chat "owner" property to null
                allOwnedChats.ForEach(c => c.OwnerUser = null);
            }
            catch
            {
                // if it not works
                return false;
            }
            // if it works
            return true;
        }

        /// <summary>
        /// Installed property <see cref="Chat.Member"/> to <see cref="null"/> 
        /// in all chats in which the user is a member.
        /// May be necessary for deletion from a database 
        /// that is not configured for cascading delete
        /// </summary>
        /// <param name="user">User you want to replace with null</param>
        /// <returns></returns>
        public bool SetMemberPropertyToNullInChats(User user)
        {
            try
            {
                // find all chat where user member
                var allOwnedChats = this.GetAllUserMemberChats(user);

                // set all owned chat "owner" property to null
                allOwnedChats.ForEach(c => c.MemberUser = null);
            }
            catch
            {
                // if it not works
                return false;
            }
            // if it works
            return true;
        }

        #endregion

        #region State Management

        public bool Save()
        {
            try
            {
                this._ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task SaveAsync()
        {
            await this._ctx.SaveChangesAsync();
        }

        #endregion

        public void Dispose()
        {
            this._ctx.Dispose();
        }
    }
}
