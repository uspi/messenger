using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Messenger.Core
{
    // it's for test and debug
    public class ChatListDesignModel : ChatListViewModel
    {
        // instance of this class
        public static ChatListDesignModel Instance { get => new ChatListDesignModel(); }

        // constructor
        public ChatListDesignModel() 
        {
            //base.Items = new ObservableCollection<ChatListItemViewModel>
            //{
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "SG",
            //        Name = "Saul Goodman",
            //        Message = "Can you do something for me? The best " +
            //            "teachers are those who show you where to look, but don't tell you what to see",
            //        ProfilePictureRGB = "009be3",
            //        NewMessageAvailable = true,
            //        IsSelected = true
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "NV",
            //        Name = "Nacho Varga",
            //        Message = "Something to discuss...",
            //        ProfilePictureRGB = "45c439"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "KW",
            //        Name = "Kim Wexler",
            //        Message = "Your documents are ready!",
            //        ProfilePictureRGB = "c41888"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "SG",
            //        Name = "Saul Goodman",
            //        Message = "Can you do something for me? The best " +
            //            "teachers are those who show you where to look, but don't tell you what to see",
            //        ProfilePictureRGB = "009be3"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "NV",
            //        Name = "Nacho Varga",
            //        Message = "Something to discuss...",
            //        ProfilePictureRGB = "45c439"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "KW",
            //        Name = "Kim Wexler",
            //        Message = "Your documents are ready!",
            //        ProfilePictureRGB = "c41888"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "SG",
            //        Name = "Saul Goodman",
            //        Message = "Can you do something for me? The best " +
            //            "teachers are those who show you where to look, but don't tell you what to see",
            //        ProfilePictureRGB = "009be3"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "NV",
            //        Name = "Nacho Varga",
            //        Message = "Something to discuss...",
            //        ProfilePictureRGB = "45c439"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        ProfileInitials = "KW",
            //        Name = "Kim Wexler",
            //        Message = "Your documents are ready!",
            //        ProfilePictureRGB = "c41888"
            //    }
            //};
        }

        //public void SetItems(ObservableCollection<ChatListItemViewModel> items = null)
        //{
        //    base.Items = new List<ChatListItemViewModel>
        //    {
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "SG",
        //            Name = "Saul Goodman",
        //            Message = "Can you do something for me? The best " +
        //                "teachers are those who show you where to look, but don't tell you what to see",
        //            ProfilePictureRGB = "009be3",
        //            NewMessageAvailable = true,
        //            IsSelected = true
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "NV",
        //            Name = "Nacho Varga",
        //            Message = "Something to discuss...",
        //            ProfilePictureRGB = "45c439"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "KW",
        //            Name = "Kim Wexler",
        //            Message = "Your documents are ready!",
        //            ProfilePictureRGB = "c41888"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "SG",
        //            Name = "Saul Goodman",
        //            Message = "Can you do something for me? The best " +
        //                "teachers are those who show you where to look, but don't tell you what to see",
        //            ProfilePictureRGB = "009be3"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "NV",
        //            Name = "Nacho Varga",
        //            Message = "Something to discuss...",
        //            ProfilePictureRGB = "45c439"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "KW",
        //            Name = "Kim Wexler",
        //            Message = "Your documents are ready!",
        //            ProfilePictureRGB = "c41888"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "SG",
        //            Name = "Saul Goodman",
        //            Message = "Can you do something for me? The best " +
        //                "teachers are those who show you where to look, but don't tell you what to see",
        //            ProfilePictureRGB = "009be3"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "NV",
        //            Name = "Nacho Varga",
        //            Message = "Something to discuss...",
        //            ProfilePictureRGB = "45c439"
        //        },
        //        new ChatListItemViewModel
        //        {
        //            ProfileInitials = "KW",
        //            Name = "Kim Wexler",
        //            Message = "Your documents are ready!",
        //            ProfilePictureRGB = "c41888"
        //        }
        //    };
        //}
    }
}
