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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";

                var statusCode = error switch
                {
                    NotFoundException => HttpStatusCode.NotFound,
                    BusinessException => HttpStatusCode.BadRequest,
                    UnauthorizedException => HttpStatusCode.Unauthorized,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    ArgumentException => HttpStatusCode.BadRequest,
                    InvalidOperationException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = (int)statusCode;

                var result = ApiResponse<string>.Fail(error.Message);
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
