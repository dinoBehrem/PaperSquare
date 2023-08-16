using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Domain;

namespace PaperSquare.Core.Models.Identity
{
    public class User : IdentityUser, ISoftDelete, IAuditableEntity
    {
        public User()
        {
            Claims = new HashSet<UserClaim>();
            Roles = new HashSet<UserRole>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        #region Properties

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }

        #endregion Properties     

        #region Navigation

        public ICollection<RefreshToken> RefreshTokens { get; set; }
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

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
