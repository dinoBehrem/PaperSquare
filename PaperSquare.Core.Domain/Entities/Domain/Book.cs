using PaperSquare.Domain.Common;

namespace PaperSquare.Domain.Entities.Domain;

public sealed class Book : AuditableEntity<string>
{
    public Book(string id) : base(id) { }

    #region Properties

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }

    #endregion Properties

    #region Navigation

    public BookSeries? Series { get; set; }
    public string? SeriesId { get; set; }

    public ICollection<BookAuthors> Authors { get; set; }
    public ICollection<BookPublisher>? Publishings { get; set; }
    public ICollection<BookGenre>? Genres { get; set; }
    public ICollection<BookInShelf>? BookShelves { get; set; }
    public ICollection<Quote>? Quotes { get; set; }
    public ICollection<BookReview>? Reviews { get; set; }

    #endregion Navigation
}
