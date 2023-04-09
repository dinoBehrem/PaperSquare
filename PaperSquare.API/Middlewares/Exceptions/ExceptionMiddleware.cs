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

            var errorReponse = new ApiErrorResponse()
            {
                Source = exception.TargetSite?.DeclaringType?.FullName,
                Exception = exception.InnerException?.Message,
                SupportMesage = $"Provide the Error Id: {errorId} to the support team for further analysis."
            };

            errorReponse.Messages.Add(exception.Message);

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
                    errorReponse.StatusCode = HttpStatusCode.NotFound;
                    errorReponse.Messages.Add($"Entity of type \"{exc.Type.Name}\" was not found!");
                    break;

                case UnatuhorizedAccessException:
                    errorReponse.StatusCode = HttpStatusCode.Unauthorized;

                    break;

                case CustomException exc:
                    errorReponse.StatusCode = exc.StatusCode;

                    if (exc.ErrorMessages is not null)
                    {
                        errorReponse.Messages = exc.ErrorMessages;
                    }

                    break;
            }

            Log.Error($"{errorReponse.Exception} Request failed with Status Code {context.Response.StatusCode} and Error Id {errorId}.");

            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = (int)errorReponse.StatusCode;

                await response.WriteAsync(JsonConvert.SerializeObject(errorReponse));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}
