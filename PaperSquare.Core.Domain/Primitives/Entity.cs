namespace PaperSquare.Core.Domain.Primitives;

public abstract class Entity<TType> : IEquatable<Entity<TType>>
{
    protected Entity(TType id)
    {
        Id = id;
    }


    #region Properties

    public TType Id { get; private init; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    #endregion Properties

    #region Fields

    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    #endregion Fields

    #region Behaviour

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    #endregion Behaviour

    #region Equatable

    public bool Equals(Entity<TType>? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.Id is null)
        {
            return false;
        }

        return obj.Id.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if(obj.GetType() != GetType())
        {
            return false;
        }

        if(obj is not Entity<TType> entity)
        {
            return false;
        }

        return Equals(entity);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator == (Entity<TType>? entity, Entity<TType>? comperer)
    {
        return entity is not null && comperer is not null && entity.Equals(comperer);
    }

    public static bool operator != (Entity<TType>? entity , Entity<TType>? comperer)
    {
        return entity is not null && comperer is not null && !entity.Equals(comperer);
    }

    #endregion Equatable
}
