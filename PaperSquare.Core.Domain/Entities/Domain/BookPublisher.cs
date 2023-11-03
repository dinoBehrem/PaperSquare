using PaperSquare.Domain.Common;
using static PaperSquare.Shared.Enums.BookEnums;

namespace PaperSquare.Domain.Entities.Domain;

public sealed class BookPublisher : AuditableEntity<string>
{
    public BookPublisher(string id) : base(id) { }

    #region Properties

    public string Edition { get; set; }
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public DateTime? PublicationDate { get; set; }
    public BookFormats Format { get; set; }
    public string? Language { get; set; }

    #endregion Properties

    #region Navigation

    public Publisher Publisher { get; set; }
    public string PublisherId { get; set; }

    public Book Book { get; set; }
    public string BookId { get; set; }

    #endregion Navigation
}
