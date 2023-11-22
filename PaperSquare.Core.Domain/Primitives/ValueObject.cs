namespace PaperSquare.Core.Domain.Primitives;

public abstract class ValueObject: IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        return obj is ValueObject valueObject && ValuesAreEqual(valueObject);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Select(vo => vo != null ? vo.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
    }

    protected static bool EqualOperator(ValueObject? valueObject, ValueObject? comparedToObject)
    {
        if(valueObject is null ^ comparedToObject is null)
        {
            return false;
        }

        return valueObject.Equals(comparedToObject!) != false;
    }

    protected static bool NotEqualOperator(ValueObject? valueObject, ValueObject? comparedToObject)
    {
        return !EqualOperator(valueObject, comparedToObject);
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    private bool ValuesAreEqual(ValueObject valueObject)
    {
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }
}
