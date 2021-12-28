using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Roles;
using CR.DataAccess.Entities.Specialties;
using CR.DataAccess.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CR.DataAccess.Entities.ExpertInformations;

namespace CR.DataAccess.Context
{
    public class ApplicationContext : IdentityDbContext<User,Role,long>
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
