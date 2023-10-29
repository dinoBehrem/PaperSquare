using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public sealed class BookSeries : BaseAuditableEntity<string>
    {
        public BookSeries(string id) : base(id) { }

        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<BookSeriesFollowers> Followers { get; set; }
        public ICollection<BookSeriesReview> Reviews { get; set; }

        #endregion Navigation
    }
}
