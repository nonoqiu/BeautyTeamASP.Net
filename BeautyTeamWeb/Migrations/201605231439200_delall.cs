namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FU_Relation", "FriendsPartId", "dbo.FriendsParts");
            DropForeignKey("dbo.FU_Relation", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsParts", "ObisoftUserId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendsParts", new[] { "ObisoftUserId" });
            DropIndex("dbo.FU_Relation", new[] { "FriendsPartId" });
            DropIndex("dbo.FU_Relation", new[] { "ObisoftUserId" });
            DropTable("dbo.FriendsParts");
            DropTable("dbo.FU_Relation");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FU_Relation",
                c => new
                    {
                        FU_RelationId = c.Int(nullable: false, identity: true),
                        FriendsPartId = c.Int(nullable: false),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FU_RelationId);
            
            CreateTable(
                "dbo.FriendsParts",
                c => new
                    {
                        FriendsPartId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FriendsPartId);
            
            CreateIndex("dbo.FU_Relation", "ObisoftUserId");
            CreateIndex("dbo.FU_Relation", "FriendsPartId");
            CreateIndex("dbo.FriendsParts", "ObisoftUserId");
            AddForeignKey("dbo.FriendsParts", "ObisoftUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FU_Relation", "ObisoftUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FU_Relation", "FriendsPartId", "dbo.FriendsParts", "FriendsPartId", cascadeDelete: true);
        }
    }
}
