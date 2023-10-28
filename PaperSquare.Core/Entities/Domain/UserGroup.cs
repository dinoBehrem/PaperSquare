using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public class UserGroup : BaseAuditableEntity<string>
    {
        #region Properties

        public string Name { get; set; }
        public string? Description { get; set; }

        #endregion Properties

        #region Navigation

        public ICollection<GroupMembership> Members { get; set; }
        public ICollection<GroupMembershipRequest> MembershipRequests { get; set; }

        #endregion Navigation
    }
}
