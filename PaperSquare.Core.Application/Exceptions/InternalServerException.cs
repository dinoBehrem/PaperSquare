using System.Net;

namespace PaperSquare.Core.Application.Exceptions;

public class InternalServerException : CustomException
{
    public InternalServerException(string message, List<string>? errors = default) : base(message, errors, HttpStatusCode.InternalServerError)
    {

    }
}
