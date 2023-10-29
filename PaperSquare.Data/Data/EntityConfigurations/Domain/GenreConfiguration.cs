using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name).IsRequired();

            builder.HasMany(g => g.Books)
                .WithOne(bg => bg.Genre)
                .HasForeignKey(bg => bg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(g => g.Users)
                .WithOne(ug => ug.Genre)
                .HasForeignKey(ug => ug.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
