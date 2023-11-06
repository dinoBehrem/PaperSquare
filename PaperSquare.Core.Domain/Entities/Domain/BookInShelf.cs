using PaperSquare.Core.Domain.Common;
using static PaperSquare.Shared.Enums.BookEnums;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class BookInShelf : AuditableEntity<string>
{
    public BookInShelf(string id) : base(id) { }

    #region Properties

    public decimal? Progress { get; set; }
    public BookStatus Status { get; set; }

    #endregion Properties

    #region Navigation

    public BookShelf BookShelf { get; set; }
    public string BookShelfId { get; set; }

    public Book Book { get; set; }
    public string BookId { get; set; }

    #endregion Navigation
}
