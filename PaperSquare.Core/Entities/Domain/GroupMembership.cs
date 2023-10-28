using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;
using static PaperSquare.Shared.Enums.UserEnums;

namespace PaperSquare.Domain.Entities.Domain
{
    public class GroupMembership : BaseAuditableEntity<string>
    {
        #region Properties

        public GroupMembershipRole Role { get; set; }

        #endregion Properties

        #region Navigation

        public UserGroup Group { get; set; }
        public string GroupId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        #endregion Navigation
    }
}
