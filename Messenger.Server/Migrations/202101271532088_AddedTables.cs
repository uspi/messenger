namespace Messenger.Server
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        IsChannel = c.Boolean(nullable: false),
                        MemberUser_Id = c.Long(),
                        OwnerUser_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.MemberUser_Id)
                .ForeignKey("dbo.Users", t => t.OwnerUser_Id)
                .Index(t => t.MemberUser_Id)
                .Index(t => t.OwnerUser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 32),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Nick = c.String(nullable: false, maxLength: 32),
                        FirstName = c.String(maxLength: 32),
                        LastName = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChatId = c.Long(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Text = c.String(),
                        AuthorUser_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorUser_Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId)
                .Index(t => t.AuthorUser_Id);
            
            CreateTable(
                "dbo.MessageReadeds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ReadedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Message_Id = c.Long(nullable: false),
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.Message_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Message_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageReadeds", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageReadeds", "Message_Id", "dbo.Messages");
            DropForeignKey("dbo.Chats", "OwnerUser_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Messages", "AuthorUser_Id", "dbo.Users");
            DropForeignKey("dbo.Chats", "MemberUser_Id", "dbo.Users");
            DropIndex("dbo.MessageReadeds", new[] { "User_Id" });
            DropIndex("dbo.MessageReadeds", new[] { "Message_Id" });
            DropIndex("dbo.Messages", new[] { "AuthorUser_Id" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Chats", new[] { "OwnerUser_Id" });
            DropIndex("dbo.Chats", new[] { "MemberUser_Id" });
            DropTable("dbo.MessageReadeds");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.Chats");
        }
    }
}
