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
                        Member_Id = c.Long(),
                        Owner_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Member_Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Member_Id)
                .Index(t => t.Owner_Id);
            
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
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Text = c.String(),
                        AuthorUser_Id = c.Long(),
                        TargetChat_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorUser_Id)
                .ForeignKey("dbo.Chats", t => t.TargetChat_Id, cascadeDelete: true)
                .Index(t => t.AuthorUser_Id)
                .Index(t => t.TargetChat_Id);
            
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
            DropForeignKey("dbo.Messages", "TargetChat_Id", "dbo.Chats");
            DropForeignKey("dbo.Messages", "AuthorUser_Id", "dbo.Users");
            DropForeignKey("dbo.Chats", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Chats", "Member_Id", "dbo.Users");
            DropIndex("dbo.MessageReadeds", new[] { "User_Id" });
            DropIndex("dbo.MessageReadeds", new[] { "Message_Id" });
            DropIndex("dbo.Messages", new[] { "TargetChat_Id" });
            DropIndex("dbo.Messages", new[] { "AuthorUser_Id" });
            DropIndex("dbo.Chats", new[] { "Owner_Id" });
            DropIndex("dbo.Chats", new[] { "Member_Id" });
            DropTable("dbo.MessageReadeds");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.Chats");
        }
    }
}
