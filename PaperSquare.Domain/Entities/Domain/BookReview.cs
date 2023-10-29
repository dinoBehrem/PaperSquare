using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain
{
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
}
