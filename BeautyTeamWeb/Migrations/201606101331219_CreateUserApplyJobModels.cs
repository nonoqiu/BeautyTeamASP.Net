namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserApplyJobModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserApplyJobModels",
                c => new
                    {
                        UserApplyJobModelId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                        JobId = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.UserApplyJobModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserApplyJobModels", "ObisoftUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserApplyJobModels", new[] { "ObisoftUserId" });
            DropTable("dbo.UserApplyJobModels");
        }
    }
}
