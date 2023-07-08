using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;
using PaperSquare.Core.Models.Identity;

namespace PaperSquare.Core.Models.Domain
{
    public class Quote: BaseEntity, IAuditableEntity, ISoftDelete
    {
        #region Properties

        public string Content { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsDeleted { get; set; }

        #endregion Properties

        #region Navigation

        public Book Book { get; set; }
        public string BookId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
        
        public QuoteCollection QuoteCollection { get; set; }
        public string QuoteCollectionId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
