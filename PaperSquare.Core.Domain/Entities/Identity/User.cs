using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Core.Domain.Entities.Domain;
using System.Security.Cryptography;

namespace PaperSquare.Core.Domain.Entities.Identity;

public sealed class User : IdentityUser, IAggregate, ISoftDelete, IAuditableEntity
{
    public User() { }

    public User(PersonalInfo personalInfo, string username, string email) : base(username)
    {
        PersonalInfo = personalInfo;
        UserName = username;
        Email = email;
        IsDeleted = false;
    }

    #region Properties

    public PersonalInfo PersonalInfo { get; private set; }

    #endregion Properties     

    #region Fields

    private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
    private readonly List<UserClaim> _claims = new List<UserClaim>();
    private readonly List<UserRole> _roles = new List<UserRole>();
    private readonly List<UserLogin> _logins = new List<UserLogin>();
    private readonly List<UserToken> _tokens = new List<UserToken>();
    private readonly List<UserGenre> _genres = new List<UserGenre>();
    private readonly List<BookShelf> _shelves = new List<BookShelf>();
    private readonly List<QuoteCollection> _quotes = new List<QuoteCollection>();
    private readonly List<GroupMembership> _memberships = new List<GroupMembership>();
    private readonly List<GroupMembershipRequest> _membershipRequests = new List<GroupMembershipRequest>();
    private readonly List<GroupMembershipRequest> _approvedMembershipRequests = new List<GroupMembershipRequest>();
    private readonly List<BookSeriesFollower> _bookSeries = new List<BookSeriesFollower>();
    private readonly List<PublisherFollower> _publishers = new List<PublisherFollower>();
    private readonly List<BookReview> _bookReviewws = new List<BookReview>();
    private readonly List<BookSeriesReview> _bookSeriesReviewws = new List<BookSeriesReview>();

    #endregion Fields

    #region Navigation

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
    public IReadOnlyCollection<UserClaim> Claims => _claims;
    public ICollection<UserRole> Roles => _roles;
    public ICollection<UserLogin> Logins => _logins;
    public ICollection<UserToken> Tokens => _tokens;
    public ICollection<UserGenre> Genres => _genres;
    public ICollection<BookShelf> Shelves => _shelves;
    public ICollection<QuoteCollection> QuoteCollections => _quotes;
    public ICollection<GroupMembership> Memberships => _memberships;
    public ICollection<GroupMembershipRequest> MembershipRequests => _membershipRequests;
    public ICollection<GroupMembershipRequest> ApprovedMembershipRequests => _approvedMembershipRequests;
    public ICollection<BookSeriesFollower> BookSeries => _bookSeries;
    public ICollection<PublisherFollower> Publishers => _publishers;
    public ICollection<BookReview> BookReviews => _bookReviewws;
    public ICollection<BookSeriesReview> BookSeriesReviews => _bookSeriesReviewws;

    #endregion Navigation

    #region Audit

    public string CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }

    #endregion Audit

    #region Methods

    public RefreshToken AddRefreshToken(DateTime expiriationDate)
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var refreshToken = new RefreshToken(Guid.NewGuid().ToString(), Convert.ToBase64String(randomNumber), expiriationDate);

        _refreshTokens.Add(refreshToken);

        return refreshToken;
    }

    public void SetPersonalInfo(string firstName, string lastName, DateTime birthdate)
    {
        if(!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
        {
            PersonalInfo = PersonalInfo.Create(firstName, lastName, birthdate);
        }
    }
    
    public void SetEmail(string email)
    {
        // TO DO: Check if email is valid
        if(!string.IsNullOrWhiteSpace(email))
        {
            Email = email;
            NormalizedEmail = email.ToUpper();
        }
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }

    #endregion Methods
}
