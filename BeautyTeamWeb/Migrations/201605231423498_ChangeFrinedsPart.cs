namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFrinedsPart : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FriendsParts", name: "ObisoftUser_Id", newName: "ObisoftUserId");
            RenameIndex(table: "dbo.FriendsParts", name: "IX_ObisoftUser_Id", newName: "IX_ObisoftUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FriendsParts", name: "IX_ObisoftUserId", newName: "IX_ObisoftUser_Id");
            RenameColumn(table: "dbo.FriendsParts", name: "ObisoftUserId", newName: "ObisoftUser_Id");
        }
    }
}
