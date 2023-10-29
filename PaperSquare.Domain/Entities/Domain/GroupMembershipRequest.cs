using PaperSquare.Core.Infrastructure;
using PaperSquare.Domain.Common;
using PaperSquare.Domain.Entities.Identity;
using static PaperSquare.Shared.Enums.UserEnums;

namespace PaperSquare.Domain.Entities.Domain
{
    public sealed class GroupMembershipRequest : BaseAuditableEntity<string>
    {
        public GroupMembershipRequest(string id) : base(id) { }

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
    }
}
