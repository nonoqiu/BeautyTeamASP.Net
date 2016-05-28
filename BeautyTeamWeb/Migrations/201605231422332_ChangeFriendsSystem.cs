namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFriendsSystem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "FriendsPartId", "dbo.FriendsParts");
            DropIndex("dbo.AspNetUsers", new[] { "FriendsPartId" });
            AddColumn("dbo.FriendsParts", "ObisoftUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FriendsParts", "ObisoftUser_Id");
            AddForeignKey("dbo.FriendsParts", "ObisoftUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendsParts", "ObisoftUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FriendsParts", new[] { "ObisoftUser_Id" });
            DropColumn("dbo.FriendsParts", "ObisoftUser_Id");
            CreateIndex("dbo.AspNetUsers", "FriendsPartId");
            AddForeignKey("dbo.AspNetUsers", "FriendsPartId", "dbo.FriendsParts", "FriendsPartId", cascadeDelete: true);
        }
    }
}
