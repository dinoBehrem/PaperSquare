using Newtonsoft.Json;
using PaperSquare.Infrastructure.Exceptions;
using Serilog;
using System.Net;
using System.Reflection.Metadata;

namespace PaperSquare.API.Middlewares.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception exc)
			{

				await HandleException(exc, context);
			}
        }

        private async Task HandleException(Exception exception, HttpContext context)
        {
            string errorId = Guid.NewGuid().ToString();

            var errorResponse = new ApiErrorResponse()
            {
                Exception = exception.Message,
                SupportMesage = $"Provide the Error Id: {errorId} to the support team for further analysis."
            };

            errorResponse.Messages.Add(exception.Message);

            if (exception.InnerException is not CustomException && exception.InnerException is not null)
            {
                while(exception.InnerException is not null)
                {
                    exception = exception.InnerException;
                }
            }

            switch (exception)
            {
                case NotFoundEntityException exc:
                    errorResponse.StatusCode = HttpStatusCode.NotFound;
                    errorResponse.Messages.Add($"Entity of type '{exc.Type.Name}' was not found!");
                    break;

                case UnatuhorizedAccessException exc:
                    errorResponse.StatusCode = HttpStatusCode.Unauthorized;
                    errorResponse.Messages.Add($"You are not authorized to access resource!");
                    break;
                
                case ErrorException exc:
                    errorResponse.StatusCode = HttpStatusCode.BadRequest;
                    errorResponse.Messages.Add($"Error at code execution!");
                    break;
                    
                case IdentityResultErrorException exc:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Messages.Add($"Identity error!");
                    break;

                case CustomException exc:
                    errorResponse.StatusCode = exc.StatusCode;

                    if (exc.ErrorMessages is not null)
                    {
                        errorResponse.Messages = exc.ErrorMessages;
                    }

                    break;
            }

            Log.Error($"{errorResponse.Exception} Request failed with Status Code {context.Response.StatusCode} and Error Id {errorId}.");

            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = (int)errorResponse.StatusCode;

                await response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}
