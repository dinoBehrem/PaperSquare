using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class PublisherFollower : AuditableEntity<string>
{
    public PublisherFollower(string id) : base(id) { }

    #region Navigation

    public User User { get; set; }
    public string UserId { get; set; }

    public Publisher Publisher { get; set; }
    public string PublisherId { get; set; }

    #endregion Navigation
}
