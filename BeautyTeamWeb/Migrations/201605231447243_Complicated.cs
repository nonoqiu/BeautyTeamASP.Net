namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Complicated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendsParts",
                c => new
                    {
                        FriendsPartId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FriendsPartId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.FU_Relation",
                c => new
                    {
                        FU_RelationId = c.Int(nullable: false, identity: true),
                        FriendsPartId = c.Int(nullable: false),
                        FriendId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FU_RelationId)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .ForeignKey("dbo.FriendsParts", t => t.FriendsPartId, cascadeDelete: true)
                .Index(t => t.FriendsPartId)
                .Index(t => t.FriendId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendsParts", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FU_Relation", "FriendsPartId", "dbo.FriendsParts");
            DropForeignKey("dbo.FU_Relation", "FriendId", "dbo.AspNetUsers");
            DropIndex("dbo.FU_Relation", new[] { "FriendId" });
            DropIndex("dbo.FU_Relation", new[] { "FriendsPartId" });
            DropIndex("dbo.FriendsParts", new[] { "ObisoftUserId" });
            DropTable("dbo.FU_Relation");
            DropTable("dbo.FriendsParts");
        }
    }
}
