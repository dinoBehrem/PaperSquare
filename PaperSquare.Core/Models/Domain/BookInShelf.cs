using PaperSquare.Core.Infrastructure;
using static PaperSquare.Shared.Enums.BookEnums;

namespace PaperSquare.Core.Models.Domain
{
    public class BookInShelf: IAuditableEntity
    {
        #region Properties

        public decimal? Progress { get; set; }
        public BookStatus Status { get; set; }

        #endregion Properties

        #region Navigation

        public BookShelf BookShelf { get; set; }
        public string BookShelfId { get; set; }

        public Book Book { get; set; }
        public string BookId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
