using PaperSquare.Core.Domain.Entities.Identity;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class BookReview : Review
{
    public BookReview(string id) : base(id) { }

    #region Navigation

    public User User { get; set; }
    public string UserId { get; set; }

    public Book Book { get; set; }
    public string BookId { get; set; }

    #endregion Navigation
}
