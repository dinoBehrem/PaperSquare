using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Firstname).IsRequired();
        builder.Property(a => a.Lastname).IsRequired();
        builder.Property(a => a.Biography).IsRequired(false);
        builder.Property(a => a.Birthdate).IsRequired();

        builder.HasMany(a => a.Books)
            .WithOne(ba => ba.Author)
            .HasForeignKey(ba => ba.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(a => a.BookSeries)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
