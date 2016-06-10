namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateViewTImes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOpenings", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobOpenings", "Views");
        }
    }
}
