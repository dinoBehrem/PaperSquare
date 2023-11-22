using Ardalis.Result;
using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Core.Domain;

public sealed class PersonalInfo : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime Birthdate { get; }

    private PersonalInfo(){}
    
    private PersonalInfo(string firstName, string lastName, DateTime birthdate)
    {
        FirstName = firstName;
        LastName = lastName;
        Birthdate = birthdate;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Birthdate;
    }

    public static Result<PersonalInfo> Create(string firstName, string lastName, DateTime birthdate)
    {
        if(firstName.Length > PersonalInfoConstraints.Length)
        {
            throw new Exception($"First name can contain up to {PersonalInfoConstraints.Length} characters!");
        }
        
        if(lastName.Length > PersonalInfoConstraints.Length)
        {
            throw new Exception($"Last name can contain up to {PersonalInfoConstraints.Length} characters!");
        }

        if(birthdate.Year - DateTime.UtcNow.Year < PersonalInfoConstraints.MinimalAge)
        {
            throw new Exception($"You need to over {PersonalInfoConstraints.MinimalAge} to create account!");
        }

        return new PersonalInfo(firstName, lastName, birthdate);
    }
}