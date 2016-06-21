namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LittleChange1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LightOfTheories", "YourName", c => c.String(nullable: false));
            AlterColumn("dbo.LightOfTheories", "TeamMembers", c => c.String(nullable: false));
            AlterColumn("dbo.LightOfTheories", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.LightOfTheories", "Class", c => c.String(nullable: false));
            AlterColumn("dbo.LightOfTheories", "Messages", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LightOfTheories", "Messages", c => c.String());
            AlterColumn("dbo.LightOfTheories", "Class", c => c.String());
            AlterColumn("dbo.LightOfTheories", "Phone", c => c.String());
            AlterColumn("dbo.LightOfTheories", "TeamMembers", c => c.String());
            AlterColumn("dbo.LightOfTheories", "YourName", c => c.String());
        }
    }
}
