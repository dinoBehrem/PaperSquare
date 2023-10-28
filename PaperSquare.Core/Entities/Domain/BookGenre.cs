using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public class BookGenre : BaseAuditableEntity<string>
    {
        #region Navigation

        public Book Book { get; set; }
        public string BookId { get; set; }

        public Genre Genre { get; set; }
        public string GenreId { get; set; }

        #endregion Navigation
    }
}
