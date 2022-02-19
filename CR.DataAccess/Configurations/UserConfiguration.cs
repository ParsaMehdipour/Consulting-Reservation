using CR.DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(p => p.ConsumerInfromation)
                .WithOne(p => p.Consumer)
                .HasForeignKey<User>(p => p.ConsumerInformationId);

            builder.HasOne(p => p.ExpertInformation)
                .WithOne(p => p.Expert)
                .HasForeignKey<User>(p => p.ExpertInformationId);

            builder.HasMany(p => p.Favorites)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

        }
    }
}
