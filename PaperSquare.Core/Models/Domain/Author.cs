using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;

namespace PaperSquare.Core.Models.Domain
{
    public class Author: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Biography { get; set; }
        public DateTime Birthdate { get; set; }

        #endregion Properties

        #region Navigation

        public ICollection<BookAuthors> Books { get; set; }
        public ICollection<BookSeries> BookSeries { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
