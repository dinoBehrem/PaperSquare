namespace PaperSquare.Core.Domain.Primitives;

public sealed class OutboxMessage
{
    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public string Type { get; init; }
    public string Data {  get; init; }

    public bool IsProcessed { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }

    public OutboxMessage(string type, string data)
    {
        Id = Guid.NewGuid();
        CreatedOnUtc = DateTime.UtcNow;
        Type = type;
        Data = data;
        IsProcessed = false;
    }

    public void MarkAsProcessed()
    {
        IsProcessed = true;
        ProcessedOnUtc = DateTime.UtcNow;
    }
}
