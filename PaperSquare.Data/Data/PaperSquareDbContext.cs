using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Domain.Entities.Domain;
using System.Reflection;

namespace PaperSquare.Data.Data;

public class PaperSquareDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public PaperSquareDbContext(DbContextOptions<PaperSquareDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<BookSeries> BookSeries { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    public virtual DbSet<BookPublisher> BookPublishers { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<BookGenre> BookGenres { get; set; }
    public virtual DbSet<UserGenre> UserGenres { get; set; }
    public virtual DbSet<BookShelf> BookShelves { get; set; }
    public virtual DbSet<BookInShelf> BookInShelves { get; set; }
    public virtual DbSet<Quote> Quotes { get; set; }
    public virtual DbSet<QuoteCollection> QuoteCollections { get; set; }
    public virtual DbSet<UserGroup> UserGroups { get; set; }
    public virtual DbSet<GroupMembership> GroupMemberships { get; set; }
    public virtual DbSet<BookAuthor> BookAuthors { get; set; }
    public virtual DbSet<GroupMembershipRequest> GroupMembershipsRequests { get; set; }
    public virtual DbSet<PublisherFollower> PublisherFollowers { get; set; }
    public virtual DbSet<BookSeriesFollower> BookSeriesFollowers { get; set; }
    public virtual DbSet<BookReview> BookReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
