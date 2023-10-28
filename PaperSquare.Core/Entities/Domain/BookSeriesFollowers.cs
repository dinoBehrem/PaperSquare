using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain
{
    public class BookSeriesFollowers : BaseAuditableEntity<string>
    {
        #region Navigation

        public User Follower { get; set; }
        public string FollowerId { get; set; }

        public BookSeries BookSeries { get; set; }
        public string BookSeriesId { get; set; }

        #endregion Navigation
    }
}
