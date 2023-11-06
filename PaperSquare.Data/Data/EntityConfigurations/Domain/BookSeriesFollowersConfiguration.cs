using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class BookSeriesFollowersConfiguration : IEntityTypeConfiguration<BookSeriesFollower>
{
    public void Configure(EntityTypeBuilder<BookSeriesFollower> builder)
    {
        builder.HasKey(bsf => new { bsf.FollowerId, bsf.BookSeriesId });

        builder.HasOne(bsf => bsf.Follower)
               .WithMany(u => u.BookSeries)
               .HasForeignKey(bsf => bsf.FollowerId)
               .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(bsf => bsf.BookSeries)
               .WithMany(bs => bs.Followers)
               .HasForeignKey(bsf => bsf.BookSeriesId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
