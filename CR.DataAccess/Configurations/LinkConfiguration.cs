using CR.DataAccess.Entities.Links;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.Property(_ => _.PersianTitle).HasMaxLength(100).IsRequired();

            builder.HasOne(_ => _.Parent)
                .WithMany(_ => _.Children)
                .HasForeignKey(_ => _.ParentLinkId);
        }
    }
}
