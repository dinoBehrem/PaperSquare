using System.Net;

namespace PaperSquare.Core.Application.Exceptions;

public class UnatuhorizedAccessException : CustomException
{
    public UnatuhorizedAccessException(string message) : base(message, null, HttpStatusCode.Unauthorized)
    {

    }
}
