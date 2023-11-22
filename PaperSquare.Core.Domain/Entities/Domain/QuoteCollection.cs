using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class QuoteCollection : AuditableEntity<string>
{
    public QuoteCollection(string id) : base(id) { }

    #region Properties

    public string Name { get; set; }

    #endregion Properties

    #region Navigation

    public User User { get; set; }
    public string UserId { get; set; }

    public ICollection<Quote> Quotes { get; set; }

    #endregion Navigation
}
