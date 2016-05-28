namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetfriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FU_Relation",
                c => new
                    {
                        FU_RelationId = c.Int(nullable: false, identity: true),
                        FriendsPartId = c.Int(nullable: false),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FU_RelationId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.FriendsPartId)
                .Index(t => t.ObisoftUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FU_Relation", "ObisoftUserId", "dbo.AspNetUsers");
            DropIndex("dbo.FU_Relation", new[] { "ObisoftUserId" });
            DropIndex("dbo.FU_Relation", new[] { "FriendsPartId" });
            DropTable("dbo.FU_Relation");
        }
    }
}
