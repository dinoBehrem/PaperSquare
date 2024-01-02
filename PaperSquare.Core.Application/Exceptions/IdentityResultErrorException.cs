namespace PaperSquare.Core.Application.Exceptions;

public class IdentityResultErrorException : CustomException
{
    public List<string> Messages { get; set; }
    public IdentityResultErrorException(params string[] messages) : base(messages.First())
    {
        Messages = messages.ToList();
    }
}
