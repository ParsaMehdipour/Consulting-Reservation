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

            builder.HasOne(b => b.ParentCategory)
                .WithMany(_ => _.SubCategories)
                .HasForeignKey(_ => _.ParentCategoryId);

            builder.HasMany(_ => _.Blogs)
                .WithOne(_ => _.BlogCategory)
                .HasForeignKey(_ => _.BlogCategoryId);

            builder.Property(_ => _.Description)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(_ => _.MetaDescription)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(_ => _.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(_ => _.ShowOrder)
                .IsRequired();

            builder.Property(_ => _.Slug)
                .HasMaxLength(150)
                .IsRequired();

        }
    }
}
