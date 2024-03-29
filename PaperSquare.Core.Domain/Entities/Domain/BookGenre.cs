﻿using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class BookGenre : AuditableEntity<string>
{
    public BookGenre(string id) : base(id) { }

    #region Navigation

    public Book Book { get; set; }
    public string BookId { get; set; }

    public Genre Genre { get; set; }
    public string GenreId { get; set; }

    #endregion Navigation
}
