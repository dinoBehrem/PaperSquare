using System.Net;

namespace PaperSquare.Core.Application.Exceptions;

public class BadRequestException : CustomException
{
    public BadRequestException(string message) : base(message, statusCode: HttpStatusCode.BadRequest)
    {
    }
}
