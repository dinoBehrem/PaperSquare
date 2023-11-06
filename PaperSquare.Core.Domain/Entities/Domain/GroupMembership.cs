using PaperSquare.Core.Domain.Common;
using PaperSquare.Core.Domain.Entities.Identity;
using static PaperSquare.Shared.Enums.UserEnums;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class GroupMembership : AuditableEntity<string>
{
    public GroupMembership(string id) : base(id) { }

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
