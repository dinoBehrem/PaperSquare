using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Domain;
using static PaperSquare.Shared.Enums.UserEnums;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class GroupMembershipRequestsConfiguration : IEntityTypeConfiguration<GroupMembershipRequest>
    {
        public void Configure(EntityTypeBuilder<GroupMembershipRequest> builder)
        {
            builder.HasKey(gmr => new { gmr.RequesterId, gmr.UserGroupId });

            builder.Property(gmr => gmr.RequestStatus).IsRequired().HasDefaultValue(GroupMembershipRequestStatus.Pending);
            builder.Property(gmr => gmr.RequestType).IsRequired();

            builder.HasOne(gmr => gmr.Requester)
                .WithMany(u => u.MembershipRequests)
                .HasForeignKey(gmr => gmr.RequesterId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(gmr => gmr.Approver)
                .WithMany(u => u.ApprovedMembershipRequests)
                .HasForeignKey(gmr => gmr.ApproverId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(gmr => gmr.UserGroup)
                .WithMany(ug => ug.MembershipRequests)
                .HasForeignKey(gmr => gmr.UserGroupId)
                .OnDelete(DeleteBehavior.NoAction);            
        }
    }
}
