namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendsParts",
                c => new
                    {
                        FriendsPartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.FriendsPartId);
            
            AddColumn("dbo.AspNetUsers", "FriendsPartId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "FriendsPartId");
            AddForeignKey("dbo.AspNetUsers", "FriendsPartId", "dbo.FriendsParts", "FriendsPartId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "FriendsPartId", "dbo.FriendsParts");
            DropIndex("dbo.AspNetUsers", new[] { "FriendsPartId" });
            DropColumn("dbo.AspNetUsers", "FriendsPartId");
            DropTable("dbo.FriendsParts");
        }
    }
}
