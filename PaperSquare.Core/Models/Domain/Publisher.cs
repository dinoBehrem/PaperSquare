using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;

namespace PaperSquare.Core.Models.Domain
{
    public class Publisher: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Name { get; set; }
        public string? Descritpion { get; set; }

        #endregion Properties

        #region Navigation

        public ICollection<BookPublisher>? Publishings { get; set; }
        public ICollection<PublisherFollower>? Followers { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
