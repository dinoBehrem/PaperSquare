using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain
{
    public class PublisherFollower : BaseAuditableEntity<string>
    {
        #region Navigation

        public User User { get; set; }
        public string UserId { get; set; }

        public Publisher Publisher { get; set; }
        public string PublisherId { get; set; }

        #endregion Navigation
    }
}
