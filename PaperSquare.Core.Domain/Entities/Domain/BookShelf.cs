using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain;

public sealed class BookShelf : AuditableEntity<string>
{
    public BookShelf(string id) : base(id) { }

    #region Properties

    public string Name { get; set; }

    #endregion Properties

    #region Navigation

    public User User { get; set; }
    public string UserId { get; set; }

    public ICollection<BookInShelf>? Books { get; set; }

    #endregion Navigation
}