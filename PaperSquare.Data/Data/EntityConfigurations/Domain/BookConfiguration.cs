﻿using Microsoft.EntityFrameworkCore;
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
            
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(b => b.Series)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.SeriesId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(b => b.Publishings)
                .WithOne(bp => bp.Book)
                .HasForeignKey(bp => bp.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.Genres)
                .WithOne(bg => bg.Book)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.BookShelves)
                .WithOne(bis => bis.Book)
                .HasForeignKey(bis => bis.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
