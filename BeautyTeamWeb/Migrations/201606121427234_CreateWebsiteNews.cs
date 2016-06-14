namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWebsiteNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsViewModels",
                c => new
                    {
                        NewsViewModelId = c.Int(nullable: false, identity: true),
                        NewsViewModelName = c.String(),
                        NewsContent = c.String(),
                        PublishTime = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsViewModelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsViewModels");
        }
    }
}
