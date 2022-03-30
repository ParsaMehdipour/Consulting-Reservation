using CR.DataAccess.Entities.ChatUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.ToTable("TBL_ChatUsers");

            builder.HasOne(_ => _.Consumer)
                .WithMany(_ => _.ChatUsers)
                .HasForeignKey(_ => _.ConsumerId);

            builder.HasOne(_ => _.ExpertInformation)
                .WithMany(_ => _.ChatUsers)
                .HasForeignKey(_ => _.ExpertInformationId);

            builder.HasMany(_ => _.ChatUserMessages)
                .WithOne(_ => _.ChatUser)
                .HasForeignKey(_ => _.ChatUserId);

            builder.Property(_ => _.AppointmentDate_String)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(_ => _.MessageType);
        }
    }
}
