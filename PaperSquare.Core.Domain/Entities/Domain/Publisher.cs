﻿using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Core.Domain.Entities.Domain;

public sealed class Publisher : AuditableEntity<string>
{
    public Publisher(string id) : base(id) { }

    #region Properties

    public string Name { get; set; }
    public string? Descritpion { get; set; }

    #endregion Properties

    #region Navigation

    public ICollection<BookPublisher>? Publishings { get; set; }
    public ICollection<PublisherFollower>? Followers { get; set; }

    #endregion Navigation
}
