using CR.DataAccess.Entities.ChatUserMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ChatUserMessageConfiguration : IEntityTypeConfiguration<ChatUserMessage>
    {
        public void Configure(EntityTypeBuilder<ChatUserMessage> builder)
        {
            builder.ToTable("TBL_ChatUserMessages");

            builder.HasOne(_ => _.ChatUser)
                .WithMany(_ => _.ChatUserMessages)
                .HasForeignKey(_ => _.ChatUserId);
        }
    }
}
