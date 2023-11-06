using PaperSquare.Core.Domain.Common;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class BookAuthor : AuditableEntity<string>
{
    public BookAuthor(string id) : base(id) { }

    #region Navigation

    public Author Author { get; set; }
    public string AuthorId { get; set; }
    public Book Book { get; set; }
    public string BookId { get; set; }

    #endregion Navigation
}
