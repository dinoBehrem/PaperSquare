namespace PaperSquare.Domain.Common
{
    public abstract class BaseEntity<TType>
    {
        public TType Id {  get; set; }
    }
}
