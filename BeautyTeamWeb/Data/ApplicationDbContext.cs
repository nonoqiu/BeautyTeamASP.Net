using BeautyTeamWeb.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BeautyTeamWeb.Models
{
    public class BeautyTeamDbContext : IdentityDbContext<ObisoftUser>
    {
        public BeautyTeamDbContext(): base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        public DbSet<EmailSubscribe> EmailSubscribes { get; set; }
        public DbSet<EU_Relation> EU_Relation { get; set; }
        public DbSet<EventLogDb> EventLogDbs { get; set; }
        public DbSet<FirComment> FirComments { get; set; }
        public DbSet<FriendsPart> FriendsParts { get; set; }
        public DbSet<FU_Relation> FU_Relation { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupTask> GroupTasks { get; set; }
        public DbSet<GU_Relation> GU_Relation { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<JobOpening> JobOpenings { get; set; }
        public DbSet<LightOfTheory> LightOfTheorys { get; set; }
        public DbSet<NewsViewModel> NewsViewModels { get; set; }
        public DbSet<PersonalEvent> PersonalEvents { get; set; }
        public DbSet<PersonalTask> PersonalTasks { get; set; }
        public DbSet<PostResponse> PostResponses { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<RadioEvent> RadioEvents { get; set; }
        public DbSet<RadioTask> RadioTasks { get; set; }
        public DbSet<SecComment> SecComments { get; set; }
        public DbSet<TU_Relation> TU_Relation { get; set; }
        public DbSet<UserApplyJobModel> UserApplyJobModels { get; set; }
    }
}