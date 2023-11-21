using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Core.Application.Shared;

public interface IPaperSquareDbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookSeries> BookSeries { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<BookPublisher> BookPublishers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<UserGenre> UserGenres { get; set; }
    public DbSet<BookShelf> BookShelves { get; set; }
    public DbSet<BookInShelf> BookInShelves { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteCollection> QuoteCollections { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<GroupMembership> GroupMemberships { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<GroupMembershipRequest> GroupMembershipRequests { get; set; }
    public DbSet<PublisherFollower> PublisherFollowers { get; set; }
    public DbSet<BookReview> BookReviews { get; set; }
}
