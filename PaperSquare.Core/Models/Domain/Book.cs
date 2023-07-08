using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;

namespace PaperSquare.Core.Models.Domain
{
    public class Book: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
       
        #endregion Properties

        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }

        public BookSeries? Series { get; set; }
        public string? SeriesId { get; set; }

        public ICollection<BookPublisher>? Publishings { get; set; }
        public ICollection<BookGenre>? Genres { get; set; }
        public ICollection<BookInShelf>? BookShelves { get; set; }
        public ICollection<Quote>? Quotes { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
