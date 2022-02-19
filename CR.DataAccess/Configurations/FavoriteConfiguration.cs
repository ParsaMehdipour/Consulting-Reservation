using CR.DataAccess.Entities.Favorites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("TBL_Favorites");

            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Favorites)
                .HasForeignKey(_ => _.UserId);

            builder.HasOne(_ => _.ExpertInformation)
                .WithMany(_ => _.Favorites)
                .HasForeignKey(_ => _.ExpertInformationId);

        }
    }
}
