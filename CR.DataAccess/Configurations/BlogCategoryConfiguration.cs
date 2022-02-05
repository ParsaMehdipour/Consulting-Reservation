using CR.DataAccess.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.ToTable("TBL_BlogCategories");

            builder.HasOne(b => b.ParentCategory).WithMany(_ => _.SubCategories).HasForeignKey(_ => _.ParentCategoryId);

        }
    }
}
