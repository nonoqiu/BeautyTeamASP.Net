namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteAnUnknownPro : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "FriendsPartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FriendsPartId", c => c.Int(nullable: false));
        }
    }
}
