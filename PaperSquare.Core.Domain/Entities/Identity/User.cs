using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Domain.Common;
using PaperSquare.Domain.Entities.Domain;
using System.Security.Cryptography;

namespace PaperSquare.Domain.Entities.Identity
{
    public sealed class User : IdentityUser, ISoftDelete, IAuditableEntity
    {
        public User(){}

        public User(string firstname, string lastname, string username, string email): base(username)
        {
            Firstname = firstname;
            Lastname = lastname;
            UserName = username;
            Email = email;
            BirthDate = DateTime.UtcNow;
            IsDeleted = false;
        }

        #region Fields

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();

        #endregion Fields

        #region Properties

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTime BirthDate { get; private set; }

        #endregion Properties     

        #region Navigation

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<UserGenre> Genres { get; set; }
        public ICollection<BookShelf> Shelves { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<QuoteCollection> QuoteCollections { get; set; }
        public ICollection<GroupMembership> Memberships { get; set; }
        public ICollection<GroupMembershipRequest> MembershipRequests { get; set; }
        public ICollection<GroupMembershipRequest> ApprovedMembershipRequests { get; set; }
        public ICollection<BookSeriesFollowers> BookSeries { get; set; }
        public ICollection<PublisherFollower> Publishers { get; set; }
        public ICollection<BookReview> BookReviews { get; set; }
        public ICollection<BookSeriesReview> BookSeriesReviews { get; set; }

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

        #endregion Methods
    }
}
