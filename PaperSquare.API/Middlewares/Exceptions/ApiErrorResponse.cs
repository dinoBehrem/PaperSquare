using PaperSquare.API.Shared;
using System.Net;

namespace PaperSquare.API.Middlewares.Exceptions
{
    public class ApiErrorResponse: BaseApiResponse
    {
        public List<string> Messages { get; set; } = new();
        public string? Exception { get; set; }
        public string? SupportMesage { get; set; }

        public ApiErrorResponse(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, bool isSuccess = false): base(statusCode, isSuccess) { }
    }
}
