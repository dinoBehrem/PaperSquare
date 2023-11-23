using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class BookSeriesReview : Review
{
    public BookSeriesReview(string id) : base(id) { }

    #region Navigation

    public User User { get; set; }
    public string UserId { get; set; }

    public BookSeries BookSeries { get; set; }
    public string BookSeriesId { get; set; }

    #endregion Navigation
}
