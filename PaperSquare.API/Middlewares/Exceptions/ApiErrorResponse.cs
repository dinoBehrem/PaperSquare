using System.Net;

namespace PaperSquare.API.Middlewares.Exceptions
{
    public class ApiErrorResponse
    {
        public List<string> Messages { get; set; } = new();
        public string? Source { get; set; }
        public string? Exception { get; set; }
        public string? SupportMesage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
