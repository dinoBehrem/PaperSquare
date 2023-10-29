using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(ug => ug.Id);

            builder.Property(ug => ug.Name).IsRequired();
            builder.Property(ug => ug.Description).IsRequired(false);

            builder.HasMany(ug => ug.Members)
                .WithOne(gm => gm.Group)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(ug => ug.MembershipRequests)
                .WithOne(gmr => gmr.UserGroup)
                .HasForeignKey(gmr => gmr.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
