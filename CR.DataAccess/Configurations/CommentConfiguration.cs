using CR.DataAccess.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("TBL_Comments");

            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Comments)
                .HasForeignKey(_ => _.UserId);

            builder.HasOne(_ => _.Parent)
                .WithMany(_ => _.Children)
                .HasForeignKey(_ => _.ParentId);
        }
    }
}
