namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addname : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FU_Relation", "FriendsPartId", "dbo.FriendsParts");
            DropIndex("dbo.FU_Relation", new[] { "FriendsPartId" });
            RenameColumn(table: "dbo.FriendsParts", name: "ObisoftUserId", newName: "ParentId");
            RenameColumn(table: "dbo.FU_Relation", name: "FriendsPartId", newName: "Parent_FriendsPartId");
            RenameIndex(table: "dbo.FriendsParts", name: "IX_ObisoftUserId", newName: "IX_ParentId");
            AddColumn("dbo.FriendsParts", "PartName", c => c.String());
            AddColumn("dbo.FU_Relation", "ParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.FU_Relation", "Parent_FriendsPartId", c => c.Int());
            CreateIndex("dbo.FU_Relation", "Parent_FriendsPartId");
            AddForeignKey("dbo.FU_Relation", "Parent_FriendsPartId", "dbo.FriendsParts", "FriendsPartId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FU_Relation", "Parent_FriendsPartId", "dbo.FriendsParts");
            DropIndex("dbo.FU_Relation", new[] { "Parent_FriendsPartId" });
            AlterColumn("dbo.FU_Relation", "Parent_FriendsPartId", c => c.Int(nullable: false));
            DropColumn("dbo.FU_Relation", "ParentId");
            DropColumn("dbo.FriendsParts", "PartName");
            RenameIndex(table: "dbo.FriendsParts", name: "IX_ParentId", newName: "IX_ObisoftUserId");
            RenameColumn(table: "dbo.FU_Relation", name: "Parent_FriendsPartId", newName: "FriendsPartId");
            RenameColumn(table: "dbo.FriendsParts", name: "ParentId", newName: "ObisoftUserId");
            CreateIndex("dbo.FU_Relation", "FriendsPartId");
            AddForeignKey("dbo.FU_Relation", "FriendsPartId", "dbo.FriendsParts", "FriendsPartId", cascadeDelete: true);
        }
    }
}
