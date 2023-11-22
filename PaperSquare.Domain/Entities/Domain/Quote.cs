using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain;

public sealed class Quote : AuditableEntity<string>
{
    public Quote(string id) : base(id) { }

    #region Properties

    public string Content { get; set; }
    public bool IsFavourite { get; set; }
    public bool IsDeleted { get; set; }

    #endregion Properties

    #region Navigation

    public Book Book { get; set; }
    public string BookId { get; set; }

    public QuoteCollection QuoteCollection { get; set; }
    public string QuoteCollectionId { get; set; }

    #endregion Navigation
}
