using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;
using PaperSquare.Core.Models.Identity;
using static PaperSquare.Shared.Enums.UserEnums;

namespace PaperSquare.Core.Models.Domain
{
    public class GroupMembershipRequest: IAuditableEntity
    {
        #region Properties

        public GroupMembershipRequestStatus RequestStatus { get; set; }
        public GroupMembershipRequestType RequestType { get; set; }
        public string? Message { get; set; }

        #endregion Properties

        #region Navigation

        public User Requester { get; set; }
        public string RequesterId { get; set; }
        
        public User? Approver { get; set; }
        public string? ApproverId { get; set; }

        public UserGroup UserGroup { get; set; }
        public string UserGroupId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
