using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Descritpion).IsRequired(false);

            builder.HasMany(b => b.Publishings)
               .WithOne(bp => bp.Publisher)
               .HasForeignKey(bp => bp.PublisherId)
               .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(p => p.Followers)
               .WithOne(pf => pf.Publisher)
               .HasForeignKey(pf => pf.PublisherId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
