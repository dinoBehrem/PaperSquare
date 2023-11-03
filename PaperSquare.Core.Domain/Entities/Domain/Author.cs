using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain;

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

    public ICollection<BookAuthors> Books { get; set; }
    public ICollection<BookSeries> BookSeries { get; set; }

    #endregion Navigation
}
