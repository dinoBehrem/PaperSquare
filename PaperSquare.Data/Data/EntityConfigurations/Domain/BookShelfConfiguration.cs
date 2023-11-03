using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Domain.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookShelfConfiguration : IEntityTypeConfiguration<BookShelf>
    {
        public void Configure(EntityTypeBuilder<BookShelf> builder)
        {
            builder.HasKey(bs => bs.Id);

            builder.Property(bs => bs.Name).IsRequired();

            builder.HasOne(bs => bs.User)
                .WithMany(u => u.Shelves)
                .HasForeignKey(bs => bs.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Books)
                .WithOne(bis => bis.BookShelf)
                .HasForeignKey(bis => bis.BookShelfId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
