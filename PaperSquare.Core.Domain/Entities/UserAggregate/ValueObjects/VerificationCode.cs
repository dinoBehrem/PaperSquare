using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Core.Domain.Entities.UserAggregate.ValueObjects;

public sealed class VerificationCode : ValueObject
{
    public string Code { get; private set; }
    public bool IsValid { get; private set; }
    public DateTime ExpiringDate { get; private set; }

    private VerificationCode() { }

    private VerificationCode(string code, DateTime? expiringDate = null, bool isValid = true)
    {
        Code = code;
        IsValid = isValid;
        ExpiringDate = expiringDate ?? DateTime.UtcNow.AddMinutes(10);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return IsValid;
        yield return ExpiringDate;
    }

    public static VerificationCode Create(string code)
    {
        if(!string.IsNullOrWhiteSpace(code))
        {
            throw new Exception($"Invalid validation code!");
        }
        
        return new VerificationCode(code);
    }

    public void MarkAsInvalid()
    {
        IsValid = false;
    }
}
