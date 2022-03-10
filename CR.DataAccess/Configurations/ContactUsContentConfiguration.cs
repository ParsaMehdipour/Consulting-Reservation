using CR.DataAccess.Entities.ContactUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ContactUsContentConfiguration : IEntityTypeConfiguration<ContactUsContent>
    {
        public void Configure(EntityTypeBuilder<ContactUsContent> builder)
        {
            builder.ToTable("TBL_ContactUsContents");
        }
    }
}
