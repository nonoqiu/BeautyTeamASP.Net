namespace BeautyTeamWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailSubscribes",
                c => new
                    {
                        EmailSubscribeId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.EmailSubscribeId);
            
            CreateTable(
                "dbo.EU_Relation",
                c => new
                    {
                        EU_RelationId = c.Int(nullable: false, identity: true),
                        GroupEventEventId = c.Int(nullable: false),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EU_RelationId)
                .ForeignKey("dbo.GroupEvents", t => t.GroupEventEventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.GroupEventEventId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.GroupEvents",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        EventName = c.String(),
                        EventContent = c.String(),
                        Before = c.Time(nullable: false, precision: 7),
                        HappenTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        ProjectName = c.String(nullable: false),
                        ProjectDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        GroupDescription = c.String(),
                        GroupType = c.Int(nullable: false),
                        GroupImage = c.String(),
                        CanNotBeSearched = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostsId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Content = c.String(nullable: false),
                        Views = c.Int(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        GroupId = c.Int(),
                        ObisoftUserId = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PostsId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.GroupId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.FirComments",
                c => new
                    {
                        FirCommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ObisoftUserId = c.String(maxLength: 128),
                        PublishDate = c.DateTime(nullable: false),
                        PostsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FirCommentId)
                .ForeignKey("dbo.Posts", t => t.PostsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId)
                .Index(t => t.PostsId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SchoolId = c.Int(),
                        SchoolAccount = c.String(),
                        SchoolPassAes = c.String(),
                        AllowSeeIfImFree = c.Boolean(nullable: false),
                        AllowSeeWhatImDoing = c.Boolean(nullable: false),
                        AllowAddtoMyCalendar = c.Boolean(nullable: false),
                        AllowSeeMyCalendar = c.Boolean(nullable: false),
                        AllowSeeMySchoolAndAccount = c.Boolean(nullable: false),
                        openid = c.String(),
                        NickName = c.String(),
                        RealName = c.String(),
                        IconImage = c.String(),
                        Description = c.String(maxLength: 150),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GU_Relation",
                c => new
                    {
                        GU_RelationId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                        GroupId = c.Int(nullable: false),
                        RelationType = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.GU_RelationId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        InvitationId = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InvitationId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.GroupId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.RadioEvents",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        RadioStationGroupId = c.Int(nullable: false),
                        EventName = c.String(),
                        EventContent = c.String(),
                        Before = c.Time(nullable: false, precision: 7),
                        HappenTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Groups", t => t.RadioStationGroupId, cascadeDelete: true)
                .Index(t => t.RadioStationGroupId);
            
            CreateTable(
                "dbo.RadioTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        RadioStationGroupId = c.Int(nullable: false),
                        TaskName = c.String(),
                        TaskContent = c.String(),
                        Before = c.Time(nullable: false, precision: 7),
                        DeadLine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Groups", t => t.RadioStationGroupId, cascadeDelete: true)
                .Index(t => t.RadioStationGroupId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PersonalEvents",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                        EventName = c.String(),
                        EventContent = c.String(),
                        Before = c.Time(nullable: false, precision: 7),
                        HappenTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.PostResponses",
                c => new
                    {
                        PostResponseId = c.Int(nullable: false, identity: true),
                        PostsId = c.Int(nullable: false),
                        ObisoftUserId = c.String(maxLength: 128),
                        PostResposneType = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PostResponseId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .ForeignKey("dbo.Posts", t => t.PostsId, cascadeDelete: true)
                .Index(t => t.PostsId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.PersonalTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        ObisoftUserId = c.String(maxLength: 128),
                        TaskName = c.String(),
                        TaskContent = c.String(),
                        Before = c.Time(nullable: false, precision: 7),
                        DeadLine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TU_Relation",
                c => new
                    {
                        TU_RelationId = c.Int(nullable: false, identity: true),
                        GroupTaskTaskId = c.Int(nullable: false),
                        ObisoftUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TU_RelationId)
                .ForeignKey("dbo.GroupTasks", t => t.GroupTaskTaskId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .Index(t => t.GroupTaskTaskId)
                .Index(t => t.ObisoftUserId);
            
            CreateTable(
                "dbo.GroupTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        TaskName = c.String(),
                        TaskContent = c.String(),
                        Before = c.Time(nullable: false, precision: 7),
                        DeadLine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.SecComments",
                c => new
                    {
                        SecCommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ObisoftUserId = c.String(maxLength: 128),
                        PublishDate = c.DateTime(nullable: false),
                        FirCommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SecCommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObisoftUserId)
                .ForeignKey("dbo.FirComments", t => t.FirCommentId, cascadeDelete: true)
                .Index(t => t.ObisoftUserId)
                .Index(t => t.FirCommentId);
            
            CreateTable(
                "dbo.EventLogDbs",
                c => new
                    {
                        EventLogDbId = c.Int(nullable: false, identity: true),
                        HappenTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        HappenPlatform = c.Short(nullable: false),
                        version = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventLogDbId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GroupEvents", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.SecComments", "FirCommentId", "dbo.FirComments");
            DropForeignKey("dbo.SecComments", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FirComments", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TU_Relation", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TU_Relation", "GroupTaskTaskId", "dbo.GroupTasks");
            DropForeignKey("dbo.GroupTasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalTasks", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostResponses", "PostsId", "dbo.Posts");
            DropForeignKey("dbo.FirComments", "PostsId", "dbo.Posts");
            DropForeignKey("dbo.PostResponses", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalEvents", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GU_Relation", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RadioTasks", "RadioStationGroupId", "dbo.Groups");
            DropForeignKey("dbo.RadioEvents", "RadioStationGroupId", "dbo.Groups");
            DropForeignKey("dbo.Invitations", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invitations", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GU_Relation", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Posts", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.EU_Relation", "ObisoftUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EU_Relation", "GroupEventEventId", "dbo.GroupEvents");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SecComments", new[] { "FirCommentId" });
            DropIndex("dbo.SecComments", new[] { "ObisoftUserId" });
            DropIndex("dbo.GroupTasks", new[] { "ProjectId" });
            DropIndex("dbo.TU_Relation", new[] { "ObisoftUserId" });
            DropIndex("dbo.TU_Relation", new[] { "GroupTaskTaskId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.PersonalTasks", new[] { "ObisoftUserId" });
            DropIndex("dbo.PostResponses", new[] { "ObisoftUserId" });
            DropIndex("dbo.PostResponses", new[] { "PostsId" });
            DropIndex("dbo.PersonalEvents", new[] { "ObisoftUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.RadioTasks", new[] { "RadioStationGroupId" });
            DropIndex("dbo.RadioEvents", new[] { "RadioStationGroupId" });
            DropIndex("dbo.Invitations", new[] { "ObisoftUserId" });
            DropIndex("dbo.Invitations", new[] { "GroupId" });
            DropIndex("dbo.GU_Relation", new[] { "GroupId" });
            DropIndex("dbo.GU_Relation", new[] { "ObisoftUserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FirComments", new[] { "PostsId" });
            DropIndex("dbo.FirComments", new[] { "ObisoftUserId" });
            DropIndex("dbo.Posts", new[] { "ObisoftUserId" });
            DropIndex("dbo.Posts", new[] { "GroupId" });
            DropIndex("dbo.Projects", new[] { "GroupId" });
            DropIndex("dbo.GroupEvents", new[] { "ProjectId" });
            DropIndex("dbo.EU_Relation", new[] { "ObisoftUserId" });
            DropIndex("dbo.EU_Relation", new[] { "GroupEventEventId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EventLogDbs");
            DropTable("dbo.SecComments");
            DropTable("dbo.GroupTasks");
            DropTable("dbo.TU_Relation");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PersonalTasks");
            DropTable("dbo.PostResponses");
            DropTable("dbo.PersonalEvents");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.RadioTasks");
            DropTable("dbo.RadioEvents");
            DropTable("dbo.Invitations");
            DropTable("dbo.GU_Relation");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FirComments");
            DropTable("dbo.Posts");
            DropTable("dbo.Groups");
            DropTable("dbo.Projects");
            DropTable("dbo.GroupEvents");
            DropTable("dbo.EU_Relation");
            DropTable("dbo.EmailSubscribes");
        }
    }
}
