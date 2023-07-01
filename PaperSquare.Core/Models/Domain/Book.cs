using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;
using static PaperSquare.Shared.Enums.BookEnums;

namespace PaperSquare.Core.Models.Domain
{
    public class Book: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public BookFormats Format { get; set; }
        public string Language { get; set; }

        #endregion Properties

        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }

        public BookSeries? Series { get; set; }
        public string? SeriesId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
