﻿using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain
{
    public sealed class Genre : BaseAuditableEntity<string>
    {
        public Genre(string id) : base(id) { }

        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public ICollection<BookGenre>? Books { get; set; }
        public ICollection<UserGenre>? Users { get; set; }

        #endregion Navigation
    }
}
