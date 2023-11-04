namespace PaperSquare.Core.Domain.Common;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
