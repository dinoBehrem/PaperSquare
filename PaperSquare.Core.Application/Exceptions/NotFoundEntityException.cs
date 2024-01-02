using System.Net;

namespace PaperSquare.Core.Application.Exceptions;

public class NotFoundEntityException : CustomException
{
    public Type Type { get; }
    public NotFoundEntityException(string message, Type type) : base(message, null, HttpStatusCode.NotFound)
    {
        Type = type;
    }
}
