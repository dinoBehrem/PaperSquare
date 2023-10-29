using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Data.Generators;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Data.Data.EntityConfigurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));
            builder.HasKey(role => role.Id);
            builder.HasMany(role => role.Claims).WithOne(roleClaim => roleClaim.Role).HasForeignKey(roleClaim => roleClaim.RoleId).IsRequired();
            builder.HasMany(role => role.Roles).WithOne(userRole => userRole.Role).HasForeignKey(userRole => userRole.RoleId).IsRequired();
        }
    }
}
