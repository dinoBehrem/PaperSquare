namespace PaperSquare.Core.Domain.Primitives;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
