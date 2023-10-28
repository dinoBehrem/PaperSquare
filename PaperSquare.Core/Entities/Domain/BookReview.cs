using PaperSquare.Core.Infrastructure;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain
{
    public class BookReview : Review
    {
        #region Navigation

        public User User { get; set; }
        public string UserId { get; set; }

        public Book Book { get; set; }
        public string BookId { get; set; }

        #endregion Navigation
    }
}
