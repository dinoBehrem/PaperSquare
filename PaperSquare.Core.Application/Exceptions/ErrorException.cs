namespace PaperSquare.Core.Application.Exceptions;

public class ErrorException : CustomException
{
    public ErrorException(string message) : base(message)
    {

    }
}
