using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;

namespace PaperSquare.Core.Models.Domain
{
    public class UserGroup: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Name { get; set; }
        public string? Description { get; set; }

        #endregion Properties

        #region Navigation

        public ICollection<GroupMembership> Members { get; set; }
        public ICollection<GroupMembershipRequest> MembershipRequests { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
