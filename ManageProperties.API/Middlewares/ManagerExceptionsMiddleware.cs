using ManagerProperties.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ManageProperties.API.Middlewares
{
    public class ManagerExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public ManagerExceptionsMiddleware(RequestDelegate next)
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
                await HandleExceptions(context, ex);
            }
        }

        private Task HandleExceptions(HttpContext context, Exception ex)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = string.Empty;

            switch (ex)
            {
                case NFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ValidException validException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validException.ValidationErrors);
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);

        }
    }

    public static class ManagerExceptionsMiddlewareExtension
    {
        public static IApplicationBuilder UseManagerExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManagerExceptionsMiddleware>();
        }
    }
}
