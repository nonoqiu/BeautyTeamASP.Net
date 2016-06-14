namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsViewModels", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsViewModels", "ImageURL");
        }
    }
}
