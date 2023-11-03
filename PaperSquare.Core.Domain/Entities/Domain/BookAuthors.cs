using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain;

public sealed class BookAuthors : AuditableEntity<string>
{
    public BookAuthors(string id) : base(id) { }

    #region Navigation

    public Author Author { get; set; }
    public string AuthorId { get; set; }
    public Book Book { get; set; }
    public string BookId { get; set; }

    #endregion Navigation
}
