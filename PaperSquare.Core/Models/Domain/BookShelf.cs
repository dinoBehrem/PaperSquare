﻿using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;
using PaperSquare.Core.Models.Identity;

namespace PaperSquare.Core.Models.Domain
{
    public class BookShelf: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public User User { get; set; }
        public string UserId { get; set; }

        public ICollection<BookInShelf>? Books { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}