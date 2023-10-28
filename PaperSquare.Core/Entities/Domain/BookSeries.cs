using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public class BookSeries : BaseAuditableEntity<string>
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<BookSeriesFollowers> Followers { get; set; }
        public ICollection<BookSeriesReviews> Reviews { get; set; }

        #endregion Navigation
    }
}
