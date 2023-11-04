using PaperSquare.Core.Domain.Common;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class Author : AuditableEntity<string>
{
    public Author(string id) : base(id) { }

    #region Properties

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Biography { get; set; }
    public DateTime Birthdate { get; set; }

    #endregion Properties

    #region Navigation

    public ICollection<BookAuthor> Books { get; set; }
    public ICollection<BookSeries> BookSeries { get; set; }

    #endregion Navigation
}
