using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Description).IsRequired(false);
            builder.Property(b => b.PublicationDate).IsRequired();
            builder.Property(b => b.Format).IsRequired();
            builder.Property(b => b.Edition).IsRequired(false);
            builder.Property(b => b.Language).IsRequired(false);
        }
    }
}
