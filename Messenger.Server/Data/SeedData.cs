using Messenger.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.Server
{
    public static class SeedData
    {
        public static void SeedDatabase(DataBaseContext context)
        {
            if (context.Users.Count() != 0)
            {
                return;
            }

            // adding users
            context.Users.AddRange(new List<User>()
            {
                new User
                {
                    Email = "admin@gmail.com",
                    Password = "qwerty",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Nick = "admin",
                    FirstName = "AdminFN",
                    LastName = "AdminLN"
                },
                new User
                {
                    Email = "rob@gmail.com",
                    Password = "qwerty",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Nick = "rob",
                    FirstName = "RobFN",
                    LastName = "RobLN"
                },
                new User
                {
                    Email = "ben@gmail.com",
                    Password = "qwerty",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Nick = "ben",
                    FirstName = "BenFN",
                    LastName = "BenLN"
                },
                new User
                {
                    Email = "moderator@gmail.com",
                    Password = "qwerty",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Nick = "moderator",
                    FirstName = "ModeratorFN",
                    LastName = "ModeratorLN"
                },
                new User
                {
                    Email = "nancy@gmail.com",
                    Password = "qwerty",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Nick = "nancy",
                    FirstName = "NancyFN",
                    LastName = "NancyLN"
                }
            });

            context.SaveChanges();

            var chats = CreateChats(context);
            context.Chats.AddRange(chats);

            context.SaveChanges();

            var messages = CreateMessages(context);
            context.Messages.AddRange(messages);
            
            context.SaveChanges();
        }

        private static List<Chat> CreateChats(DataBaseContext ctx)
        {
            var admin = ctx.GetUserByNick("admin");
            var moderator = ctx.GetUserByNick("moderator");
            var nancy = ctx.GetUserByNick("nancy");
            var rob = ctx.GetUserByNick("rob");
            var ben = ctx.GetUserByNick("ben");

            return new List<Chat>()
            {
                // admin - moderator, ben, nancy
                new Chat
                {
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsChannel = false,
                    OwnerUser = admin,
                    MemberUser = moderator
                },
                new Chat
                {
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsChannel = false,
                    OwnerUser = admin,
                    MemberUser = ben
                },
                new Chat
                {
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsChannel = false,
                    OwnerUser = admin,
                    MemberUser = nancy
                },

                // moderator - nancy
                new Chat
                {
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsChannel = false,
                    OwnerUser = moderator,
                    MemberUser = nancy
                },

                // moderator - rob
                new Chat
                {
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsChannel = false,
                    OwnerUser = moderator,
                    MemberUser = rob
                },

            
                // rob - ben
                new Chat
                {
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsChannel = false,
                    OwnerUser = rob,
                    MemberUser = ben
                }
            };
        }

        private static List<Message> CreateMessages(DataBaseContext ctx)
        {
            // users
            var admin = ctx.GetUserByNick("admin");          
            var moderator = ctx.GetUserByNick("moderator");
            var nancy = ctx.GetUserByNick("nancy");       
            var rob = ctx.GetUserByNick("rob");
            var ben = ctx.GetUserByNick("ben");

            //chats
            var adminModeratorChat = ctx.GetChatByMembers(admin, moderator);
            var adminBenChat = ctx.GetChatByMembers(admin, ben);
            var adminNancyChat = ctx.GetChatByMembers(admin, nancy);
            var moderatorNancyChat = ctx.GetChatByMembers(moderator, nancy);
            var moderatorRobChat = ctx.GetChatByMembers(moderator, rob);
            var robBenChat = ctx.GetChatByMembers(rob, ben);
             
            return new List<Message>()
            {
                // admin - moderator
                new Message
                {
                    AuthorUser = admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminModeratorChat.Id,
                    Text = "Hey Moderator, how are you man?"
                },
                new Message
                {
                    AuthorUser = moderator,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminModeratorChat.Id,
                    Text = "today I need to finish the project, so you better ask tomorrow)"
                },
                new Message
                {
                    AuthorUser = admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminModeratorChat.Id,
                    Text = "okey 1-1 "
                },

                // admin - ben
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "I think I pressed something wrong"
                },
                new Message
                {
                    AuthorUser = admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "it's ok, relax"
                },
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "ok, can you come check?"
                },
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "because i can't relax"
                },
                new Message
                {
                    AuthorUser = admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "ok wait 5 minutes"
                },
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "well I'll wait"
                },
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminBenChat.Id,
                    Text = "hurry up please"
                },

                // admin - nancy
                new Message
                {
                    AuthorUser = admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminNancyChat.Id,
                    Text = "hey, nancy!"
                },
                new Message
                {
                    AuthorUser = admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminNancyChat.Id,
                    Text = "how are you?"
                },
                new Message
                {
                    AuthorUser = nancy,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = adminNancyChat.Id,
                    Text = "I'm fine, thank you)"
                },

                // moderator - nancy
                new Message
                {
                    AuthorUser = moderator,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = moderatorNancyChat.Id,
                    Text = "hi nancy can you please throw off the document we talked about"
                },
                new Message
                {
                    AuthorUser = moderator,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = moderatorNancyChat.Id,
                    Text = "hello, now I can’t, but in the evening I think I will"
                },
                new Message
                {
                    AuthorUser = moderator,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = moderatorNancyChat.Id,
                    Text = "thank you"
                },

                // moderator - rob
                new Message
                {
                    AuthorUser = moderator,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = moderatorRobChat.Id,
                    Text = "rob when is our party going?"
                },
                new Message
                {
                    AuthorUser = rob,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = moderatorRobChat.Id,
                    Text = "come by 9:00 PM"
                },

                // rob - ben
                new Message
                {
                    AuthorUser = rob,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = robBenChat.Id,
                    Text = "ben what do you think about ideas for the program"
                },
                new Message
                {
                    AuthorUser = rob,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = robBenChat.Id,
                    Text = "essence: a program for managing a store. but not just crm but with machine learning for the selection of images for products"
                },
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = robBenChat.Id,
                    Text = "well, this is only within the budget of Google)"
                },
                new Message
                {
                    AuthorUser = ben,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ChatId = robBenChat.Id,
                    Text = "but let's discuss"
                },
            };
        }

        private static User GetUserByNick(
            this DataBaseContext ctx, string nick)
        {
            return ctx.Users.Where(u => u.Nick == nick).FirstOrDefault();
        }

        private static Chat GetChatByMembers(
            this DataBaseContext ctx, User memberOne, User memberTwo)
        {
            return ctx.Chats.Where(c => 
                (c.MemberUser.Id == memberOne.Id && c.OwnerUser.Id == memberTwo.Id)
             || (c.MemberUser.Id == memberTwo.Id && c.OwnerUser.Id == memberOne.Id)
              ).FirstOrDefault();
        }
    }
}
