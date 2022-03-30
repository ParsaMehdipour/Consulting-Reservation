using CR.DataAccess.Entities.Specialties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("TBL_Specialties");

            builder.HasMany(p => p.ExpertInformations)
                .WithOne(p => p.Specialty)
                .HasForeignKey(p => p.SpecialtyId);

            builder.Property(_ => _.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
