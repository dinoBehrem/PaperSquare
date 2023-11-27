using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class GroupMembershipConfiguration : IEntityTypeConfiguration<GroupMembership>
{
    public void Configure(EntityTypeBuilder<GroupMembership> builder)
    {
        builder.HasKey(gm => new { gm.GroupId, gm.UserId });

        builder.Property(gm => gm.Role).IsRequired();

        builder.HasOne(gm => gm.Group)
            .WithMany(ug => ug.Members)
            .HasForeignKey(gm => gm.GroupId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(gm => gm.User)
            .WithMany(u => u.Memberships)
            .HasForeignKey(gm => gm.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
