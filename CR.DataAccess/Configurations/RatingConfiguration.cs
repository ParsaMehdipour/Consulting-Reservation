using CR.DataAccess.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("TBL_Ratings");

            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Ratings)
                .HasForeignKey(_ => _.UserId);


            builder.HasOne(_ => _.Comment)
                .WithMany(_ => _.Rate)
                .HasForeignKey(_ => _.CommentId);
        }
    }
}
