namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupPostHaveOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PublisherFormGroupId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "PublisherFormGroupId");
            AddForeignKey("dbo.Posts", "PublisherFormGroupId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PublisherFormGroupId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "PublisherFormGroupId" });
            DropColumn("dbo.Posts", "PublisherFormGroupId");
        }
    }
}
