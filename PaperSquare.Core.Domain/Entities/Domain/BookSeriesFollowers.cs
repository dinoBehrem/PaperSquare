using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain;

public sealed class BookSeriesFollowers : AuditableEntity<string>
{
    public BookSeriesFollowers(string id) : base(id) { }

    #region Navigation

    public User Follower { get; set; }
    public string FollowerId { get; set; }

    public BookSeries BookSeries { get; set; }
    public string BookSeriesId { get; set; }

    #endregion Navigation
}
