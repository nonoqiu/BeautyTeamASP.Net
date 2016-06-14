namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiontoNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsViewModels", "ToAction", c => c.String());
            AddColumn("dbo.NewsViewModels", "ToController", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsViewModels", "ToController");
            DropColumn("dbo.NewsViewModels", "ToAction");
        }
    }
}
