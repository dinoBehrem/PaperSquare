using System.Net;

namespace PaperSquare.Core.Application.Exceptions;

public class CommandValidationException : CustomException
{
    public IReadOnlyDictionary<string, IEnumerable<string>> ValidationErrors { get; set; }

    public CommandValidationException(string message, IReadOnlyDictionary<string, IEnumerable<string>> validationErrors, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, errors, statusCode)
    {
        ValidationErrors = validationErrors;
    }
}
