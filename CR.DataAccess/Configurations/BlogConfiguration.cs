using CR.DataAccess.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("TBL_Blogs");

            builder.HasOne(_ => _.BlogCategory)
                .WithMany(_ => _.Blogs)
                .HasForeignKey(_ => _.BlogCategoryId);
        }
    }
}
