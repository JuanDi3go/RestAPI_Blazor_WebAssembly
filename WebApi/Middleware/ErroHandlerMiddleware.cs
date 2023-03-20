using Application.Wrappers;
using Azure;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    public class ErroHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErroHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new GenericResponse<string>() { Succes = false, Message= ex?.Message };
                switch (ex)
                {
                    case Application.Exceptions.ApiException e:
                        //custoom application error

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case Application.Exceptions.ValidationException e:

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;

                        break;

                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;

                        break;
                    default:
                        //unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        break;
                }
                var result =  JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
