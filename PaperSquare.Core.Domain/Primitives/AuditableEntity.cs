namespace PaperSquare.Core.Domain.Primitives;

public abstract class AuditableEntity<TType> : Entity<TType>, ISoftDelete, IAuditableEntity
{
    protected AuditableEntity(TType id) : base(id) { }

    public string CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
}
