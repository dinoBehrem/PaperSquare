using Microsoft.AspNetCore.Identity;
using PaperSquare.Domain.Common.Interfaces;
using PaperSquare.Domain.Entities.Domain;

namespace PaperSquare.Domain.Entities.Identity
{
    public class User : IdentityUser, ISoftDelete, IAuditableEntity
    {
        public User(string firstname, string lastname, string username, string email): base(username)
        {
            Firstname = firstname;
            Lastname = lastname;
            UserName = username;
            Email = email;
            BirthDate = DateTime.UtcNow;
            IsDeleted = false;
            

            Claims = new HashSet<UserClaim>();
            Roles = new HashSet<UserRole>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
            //RefreshTokens = new HashSet<RefreshToken>();
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

        public virtual IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
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
        public ICollection<BookSeriesReviews> BookSeriesReviews { get; set; }

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
            var refreshToken = new RefreshToken(Id, expiriationDate);

            _refreshTokens.Add(refreshToken);

            return refreshToken;
        }

        #endregion Methods
    }
}
