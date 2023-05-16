﻿using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Infrastructure;

namespace PaperSquare.Core.Models.Identity
{
    public class User : IdentityUser, ISoftDelete, IEntityStateTracking
    {
        public User()
        {
            Claims = new HashSet<UserClaim>();
            Roles = new HashSet<UserRole>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get ; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserLogin> Logins{ get; set; }
        public ICollection<UserToken> Tokens { get; set; }
    }
}
