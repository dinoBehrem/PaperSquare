using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;
using static PaperSquare.Shared.Enums.BookEnums;

namespace PaperSquare.Core.Models.Domain
{
    public class BookPublisher: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Edition { get; set; }
        public string? Description { get; set; }
        public string? Cover { get; set; }
        public DateTime? PublicationDate { get; set; }
        public BookFormats Format { get; set; }
        public string? Language { get; set; }

        #endregion Properties

        #region Navigation

        public Publisher Publisher { get; set; }
        public string PublisherId { get; set; }

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
