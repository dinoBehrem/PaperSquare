﻿using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Domain.Entities.Domain;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Domain.Primitives;

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
    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
