using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class BookSeriesFollower : AuditableEntity<string>
{
    public BookSeriesFollower(string id) : base(id) { }

    #region Navigation

    public User Follower { get; set; }
    public string FollowerId { get; set; }

    public BookSeries BookSeries { get; set; }
    public string BookSeriesId { get; set; }

    #endregion Navigation
}
