using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Identity;
using static PaperSquare.Shared.Enums.UserEnums;

namespace PaperSquare.Core.Models.Domain
{
    public class GroupMembership: IAuditableEntity
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

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
