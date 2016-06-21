namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createproducttype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        ProductTypeId = c.Int(nullable: false, identity: true),
                        ProductTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ProductTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductTypes");
        }
    }
}
