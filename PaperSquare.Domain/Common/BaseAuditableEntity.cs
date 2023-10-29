using PaperSquare.Domain.Common.Interfaces;

namespace PaperSquare.Domain.Common
{
    public abstract class BaseAuditableEntity<TType> : Entity<TType>, ISoftDelete, IAuditableEntity
    {
        protected BaseAuditableEntity(TType id) : base(id) { }

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
