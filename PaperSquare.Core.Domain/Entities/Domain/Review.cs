using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain;

public abstract class Review : AuditableEntity<string>
{
    protected Review(string id) : base(id) { }

    #region Properties

    public int Rating { get; set; }
    public string? Comment { get; set; }

    #endregion Properties
}
