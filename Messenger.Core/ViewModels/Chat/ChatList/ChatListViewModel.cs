using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messenger.Core
{
    /// <summary>
    /// The logic for the chat list. The unit of 
    /// content is <see cref="ChatListItemViewModel"/>
    /// </summary>
    public class ChatListViewModel : ViewModelBase, ICloneable
    {
        //public event EventHandler<List<ChatListItemViewModel>> ItemsChanged;

        // all user chats
        public List<ChatListItemViewModel> Items { get; set; } = new List<ChatListItemViewModel>();

        // constructor
        public ChatListViewModel() 
        {
            // start subscribe
            //Task.Run(() => SubscribeOnChatSelectedInChats());


            //if collection of chats was changed
            //Items.CollectionChanged += async (s, e) => await SubscribeOnChatSelectedInChats(s, e);
        }

        //public void SubscribeOnChatSelectedInChats(object sender, EventArgs args)
        //{
        //    // subscribe to update the flag "IsSelected" 
        //    // and his event "ChatSelected" each chat
        //    foreach (var item in Items)
        //    {
        //        //// check the condition that only 
        //        //// one chat has a flag at a time
        //        //item.ChatSelected += (s, e) =>
        //        //{
        //            // all selected chats
        //            var allSelectedChats = Items.Where(i => i.IsSelected).ToList();

        //            // all other chats except this
        //            var otherChats = allSelectedChats
        //                .Where(c => !c.Equals(sender))
        //                .ToList();

        //            // remove flags from all other chats except this one
        //            foreach (var otherChat in otherChats)
        //            {
        //                otherChat.IsSelected = false;
        //            }
        //        //};
        //    }
        //}

        public object Clone()
        {
            // empty list for cloning
            var clonedItems = new List<ChatListItemViewModel>();

            foreach (var item in Items)
            {
                clonedItems.Add((ChatListItemViewModel)item.Clone());
            }

            return 
                new ChatListViewModel 
                { 
                    Items = clonedItems
                };
        }
    }
}
