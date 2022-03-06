using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.Blogs;
using CR.DataAccess.Entities.ChatUserMessages;
using CR.DataAccess.Entities.ChatUsers;
using CR.DataAccess.Entities.Comments;
using CR.DataAccess.Entities.CommissionAndDiscounts;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.ExpertInformations;
using CR.DataAccess.Entities.Factors;
using CR.DataAccess.Entities.Favorites;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Roles;
using CR.DataAccess.Entities.Specialties;
using CR.DataAccess.Entities.Timings;
using CR.DataAccess.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CR.DataAccess.Context
{
    public class ApplicationContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ConsumerInfromation> ConsumerInfromations { get; set; }
        public DbSet<ExpertInformation> ExpertInformations { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<TimeOfDay> TimeOfDays { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<ExpertImage> ExpertImages { get; set; }
        public DbSet<ExpertExperience> ExpertExperiences { get; set; }
        public DbSet<ExpertMembership> ExpertMemberships { get; set; }
        public DbSet<ExpertPrize> ExpertPrizes { get; set; }
        public DbSet<ExpertStudy> ExpertStudies { get; set; }
        public DbSet<ExpertSubscription> ExpertSubscriptions { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<CommissionAndDiscount> CommissionAndDiscounts { get; set; }
        public DbSet<Timing> Timings { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<ChatUserMessage> ChatUserMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
