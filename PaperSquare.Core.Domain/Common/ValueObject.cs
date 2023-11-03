namespace PaperSquare.Domain.Common;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var valueObject = obj as ValueObject;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Select(vo => vo != null ? vo.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

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
}
