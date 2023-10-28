using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public class Publisher : BaseAuditableEntity<string>
    {
        #region Properties

        public string Name { get; set; }
        public string? Descritpion { get; set; }

        #endregion Properties

        #region Navigation

        public ICollection<BookPublisher>? Publishings { get; set; }
        public ICollection<PublisherFollower>? Followers { get; set; }

        #endregion Navigation
    }
}
