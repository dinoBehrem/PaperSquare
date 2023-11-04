using PaperSquare.Core.Domain.Common;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class Genre : AuditableEntity<string>
{
    public Genre(string id) : base(id) { }

    #region Properties

    public string Name { get; set; }

    #endregion Properties

    #region Navigation

    public ICollection<BookGenre>? Books { get; set; }
    public ICollection<UserGenre>? Users { get; set; }

    #endregion Navigation
}
