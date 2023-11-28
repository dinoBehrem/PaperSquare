using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Data.Data.EntityConfigurations.Identity;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.PersonalInfo, pi =>
        {
            pi.Property(pi => pi.FirstName).IsRequired();
            pi.Property(pi => pi.LastName).IsRequired();
            pi.Property(pi => pi.Birthdate).IsRequired();
        });

        builder.OwnsMany(u => u.VerificationCodes);

        builder.Property(user => user.Email).IsRequired();

        builder.HasMany(user => user.Claims)
            .WithOne(claim => claim.User)
            .HasForeignKey(userClaim => userClaim.UserId)
            .IsRequired();

        builder.HasMany(user => user.Logins)
            .WithOne(login => login.User)
            .HasForeignKey(userLogin => userLogin.UserId)
            .IsRequired();

        builder.HasMany(user => user.Roles)
            .WithOne(role => role.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        builder.HasMany(user => user.Tokens)
            .WithOne(token => token.User)
            .HasForeignKey(userToken => userToken.UserId)
            .IsRequired();

        builder.HasMany(user => user.RefreshTokens)
            .WithOne(token => token.User)
            .HasForeignKey(userToken => userToken.UserId)
            .IsRequired();

        builder.HasMany(u => u.Genres)
            .WithOne(ug => ug.User)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Shelves)
            .WithOne(bs => bs.User)
            .HasForeignKey(bs => bs.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.QuoteCollections)
            .WithOne(qc => qc.User)
            .HasForeignKey(qc => qc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Memberships)
            .WithOne(gm => gm.User)
            .HasForeignKey(gm => gm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.MembershipRequests)
            .WithOne(gmr => gmr.Requester)
            .HasForeignKey(gmr => gmr.RequesterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.ApprovedMembershipRequests)
            .WithOne(gmr => gmr.Approver)
            .HasForeignKey(gmr => gmr.ApproverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.BookSeries)
            .WithOne(bsf => bsf.Follower)
            .HasForeignKey(bsf => bsf.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Publishers)
           .WithOne(pf => pf.User)
           .HasForeignKey(pf => pf.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.BookReviews)
           .WithOne(br => br.User)
           .HasForeignKey(br => br.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.BookSeriesReviews)
           .WithOne(bsr => bsr.User)
           .HasForeignKey(bsr => bsr.UserId)
           .OnDelete(DeleteBehavior.Cascade);


        builder.HasQueryFilter(u => !u.IsDeleted);
        //builder.HasData(UsersGenerator.Generator.Users);
    }
}
