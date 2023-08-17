using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Identity;

namespace PaperSquare.Core.Models.Domain
{
    public class BookSeriesReviews: Review, IAuditableEntity
    {
        #region Navigation

        public User User { get; set; }
        public string UserId { get; set; }

        public BookSeries BookSeries { get; set; }
        public string BookSeriesId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
