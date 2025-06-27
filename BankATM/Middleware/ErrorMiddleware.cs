using BankATM.Common.Exceptions;
using BankATM.Common.Response;
using System.Net;

namespace BankATM.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext content)
        {
            try
            {
                await _next(content);
            }
            catch (Exception error)
            {
                var response = content.Response;
                HttpStatusCode statusCode = HttpStatusCode.BadRequest;

                switch (error)
                {
                    case KeyNotFoundException e:
                        statusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ArgumentException e:
                        statusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case InvalidOperationException e:
                        statusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case UnauthorizedException e:
                        statusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case Exception e:
                        statusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                content.Response.StatusCode = response.StatusCode;
                content.Response.ContentType = "application/json";
                var result = ApiResponse<string>.Fail(error.Message);
                await content.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
