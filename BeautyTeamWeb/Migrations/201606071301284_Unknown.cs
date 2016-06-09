namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unknown : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invitations", "GroupId", "dbo.Groups");
            DropIndex("dbo.Invitations", new[] { "GroupId" });
            AddColumn("dbo.Invitations", "FriendsPartId", c => c.Int());
            AddColumn("dbo.Invitations", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Invitations", "Group_GroupId", c => c.Int());
            AlterColumn("dbo.Invitations", "GroupId", c => c.Int());
            CreateIndex("dbo.Invitations", "GroupId");
            CreateIndex("dbo.Invitations", "FriendsPartId");
            CreateIndex("dbo.Invitations", "Group_GroupId");
            AddForeignKey("dbo.Invitations", "GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.Invitations", "FriendsPartId", "dbo.FriendsParts", "FriendsPartId", cascadeDelete: true);
            AddForeignKey("dbo.Invitations", "Group_GroupId", "dbo.Groups", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitations", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.Invitations", "FriendsPartId", "dbo.FriendsParts");
            DropForeignKey("dbo.Invitations", "GroupId", "dbo.Groups");
            DropIndex("dbo.Invitations", new[] { "Group_GroupId" });
            DropIndex("dbo.Invitations", new[] { "FriendsPartId" });
            DropIndex("dbo.Invitations", new[] { "GroupId" });
            AlterColumn("dbo.Invitations", "GroupId", c => c.Int(nullable: false));
            DropColumn("dbo.Invitations", "Group_GroupId");
            DropColumn("dbo.Invitations", "Discriminator");
            DropColumn("dbo.Invitations", "FriendsPartId");
            CreateIndex("dbo.Invitations", "GroupId");
            AddForeignKey("dbo.Invitations", "GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
        }
    }
}
