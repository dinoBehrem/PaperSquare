using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Identity
{
    public class RefreshToken: BaseEntity<string>
    {
        public RefreshToken(string userId, DateTime expires)
        {
            UserId = userId;
            Expires = expires;
            CreatedOnUtc = DateTime.UtcNow;
            IsValid = true;
        }

        #region Properties

        public DateTime CreatedOnUtc { get; private set; }
        public DateTime Expires { get; private set; }
        public bool IsValid { get; private set; }

        #endregion Properties

        #region Navigation

        public User User { get; private set; }
        public string UserId { get; private set; }

        #endregion Navigation

        #region Methods

        public void MarkAsInvalid()
        {
            IsValid = false;
        }

        #endregion Methods
    }
}
