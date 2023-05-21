using System.Net;

namespace PaperSquare.API.Shared
{
    public abstract class BaseApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public BaseApiResponse(HttpStatusCode statusCode, bool isSuccess)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }
    }

    partial class ApiResponse<TEntity>: BaseApiResponse
    {
        public TEntity? Data { get; set; }

        public ApiResponse(HttpStatusCode statusCode, TEntity data, bool isSuccess = true): base(statusCode, isSuccess)
        {
            Data = data;
        }
    }

}
