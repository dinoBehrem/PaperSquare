using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Shared.Enums
{
    public class UserEnums
    {
        public enum GroupMembershipRole
        {
            Admin = 1,
            Moderator = 2,
            Member = 3
        }
        
        public enum GroupMembershipRequestStatus
        {
            Pending = 1,
            Accepted = 2,
            Rejected = 3,
            Deleted = 4, // Deleted is optional since we can delete the request
        }
        
        public enum GroupMembershipRequestType
        {
            Request = 1,
            Invitation = 2,
        }
    }
}
