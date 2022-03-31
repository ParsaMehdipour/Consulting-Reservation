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

            builder.Property(_ => _.CanonicalAddress)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(_ => _.Description)
                .IsRequired();

            builder.Property(_ => _.Keywords);

            builder.Property(_ => _.MetaDescription)
                .IsRequired();

            builder.Property(_ => _.PictureSrc);

            builder.Property(_ => _.ShortDescription)
                .IsRequired();

            builder.Property(_ => _.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(_ => _.Slug)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(_ => _.ShowOrder)
                .IsRequired();

            builder.Property(_ => _.Status);
        }
    }
}
