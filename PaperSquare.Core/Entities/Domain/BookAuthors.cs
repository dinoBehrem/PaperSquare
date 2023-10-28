using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public class BookAuthors : BaseAuditableEntity<string>
    {
        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }
        public Book Book { get; set; }
        public string BookId { get; set; }

        #endregion Navigation
    }
}
