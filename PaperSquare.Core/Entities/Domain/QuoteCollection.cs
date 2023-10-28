using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Domain.Entities.Domain
{
    public class QuoteCollection : BaseAuditableEntity<string>
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public User User { get; set; }
        public string UserId { get; set; }

        public ICollection<Quote> Quotes { get; set; }

        #endregion Navigation
    }
}
