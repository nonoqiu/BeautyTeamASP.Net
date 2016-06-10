namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobOpenings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobOpenings",
                c => new
                    {
                        JobOpeningId = c.Int(nullable: false, identity: true),
                        JobOpeningName = c.String(),
                        JobDescription = c.String(),
                        PublishTime = c.DateTime(nullable: false),
                        JobType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobOpeningId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobOpenings");
        }
    }
}
