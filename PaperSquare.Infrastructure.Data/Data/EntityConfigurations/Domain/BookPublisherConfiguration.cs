using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
{
    public void Configure(EntityTypeBuilder<BookPublisher> builder)
    {
        builder.HasKey(bp => bp.Id);

        builder.Property(bp => bp.Edition).IsRequired();
        builder.Property(bp => bp.Format).IsRequired();
        builder.Property(bp => bp.PublicationDate).IsRequired(false);
        builder.Property(bp => bp.Description).IsRequired(false);
        builder.Property(bp => bp.Cover).IsRequired(false);
        builder.Property(bp => bp.Language).IsRequired(false);

        builder.HasOne(bp => bp.Publisher)
            .WithMany(p => p.Publishings)
            .HasForeignKey(bp => bp.PublisherId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(bp => bp.Book)
            .WithMany(p => p.Publishings)
            .HasForeignKey(bp => bp.BookId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
