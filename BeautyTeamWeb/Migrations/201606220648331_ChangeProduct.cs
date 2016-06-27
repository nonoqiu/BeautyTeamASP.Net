namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShortDescription");
        }
    }
}
