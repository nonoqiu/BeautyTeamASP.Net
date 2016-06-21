namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMode3l : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightOfTheories", "YourName", c => c.String());
            AddColumn("dbo.LightOfTheories", "TeamMembers", c => c.String());
            DropColumn("dbo.LightOfTheories", "TeamName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LightOfTheories", "TeamName", c => c.String());
            DropColumn("dbo.LightOfTheories", "TeamMembers");
            DropColumn("dbo.LightOfTheories", "YourName");
        }
    }
}
