using PaperSquare.Domain.Common;
using static PaperSquare.Shared.Enums.BookEnums;

namespace PaperSquare.Domain.Entities.Domain
{
    public class BookInShelf : BaseAuditableEntity<string>
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
    }
}
