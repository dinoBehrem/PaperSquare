using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;

namespace PaperSquare.Core.Models.Domain
{
    public class BookSeries: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }

        public ICollection<Book> Books { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
