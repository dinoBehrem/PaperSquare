using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class PublisherFollowerConfiguration : IEntityTypeConfiguration<PublisherFollower>
{
    public void Configure(EntityTypeBuilder<PublisherFollower> builder)
    {
        builder.HasKey(pf => new { pf.PublisherId, pf.UserId });

        builder.HasOne(pf => pf.User)
            .WithMany(u => u.Publishers)
            .HasForeignKey(pf => pf.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(pf => pf.Publisher)
            .WithMany(u => u.Followers)
            .HasForeignKey(pf => pf.PublisherId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
