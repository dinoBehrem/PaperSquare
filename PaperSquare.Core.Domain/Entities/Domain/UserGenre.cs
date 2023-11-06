using PaperSquare.Core.Domain.Common;
using PaperSquare.Core.Domain.Entities.Identity;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class UserGenre : AuditableEntity<string>
{
    public UserGenre(string id) : base(id) { }

    #region Navigation

    public User User { get; set; }
    public string UserId { get; set; }

    public Genre Genre { get; set; }
    public string GenreId { get; set; }

    #endregion Navigation
}
