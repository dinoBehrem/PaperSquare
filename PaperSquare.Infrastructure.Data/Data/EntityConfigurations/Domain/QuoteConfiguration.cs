using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Content).IsRequired();
        builder.Property(q => q.IsFavourite).HasDefaultValue(false);
        builder.Property(q => q.IsDeleted).HasDefaultValue(false);

        builder.HasOne(q => q.Book)
            .WithMany(b => b.Quotes)
            .HasForeignKey(q => q.BookId)
            .OnDelete(DeleteBehavior.NoAction);
                
        builder.HasOne(q => q.QuoteCollection)
            .WithMany(b => b.Quotes)
            .HasForeignKey(q => q.QuoteCollectionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
