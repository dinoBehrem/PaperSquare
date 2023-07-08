using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class QuoteCollectionConfiguration : IEntityTypeConfiguration<QuoteCollection>
    {
        public void Configure(EntityTypeBuilder<QuoteCollection> builder)
        {
            builder.HasKey(qc => qc.Id);

            builder.Property(qc => qc.Name).IsRequired();

            builder.HasMany(qc => qc.Quotes)
                .WithOne(q => q.QuoteCollection)
                .HasForeignKey(q => q.QuoteCollectionId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(qc => qc.User)
                .WithMany(u => u.QuoteCollections)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
