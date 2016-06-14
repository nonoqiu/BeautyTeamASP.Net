namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatchanged : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LightOfTheories",
                c => new
                    {
                        LightOfTheoryId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                        TeamName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Class = c.String(),
                        Messages = c.String(),
                    })
                .PrimaryKey(t => t.LightOfTheoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LightOfTheories", "ObisoftUserId", "dbo.AspNetUsers");
            DropIndex("dbo.LightOfTheories", new[] { "ObisoftUserId" });
            DropTable("dbo.LightOfTheories");
        }
    }
}
