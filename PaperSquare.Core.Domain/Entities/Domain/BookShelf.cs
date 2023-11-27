using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Domain.Entities.Domain;

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