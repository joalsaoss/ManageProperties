using ManagerProperties.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ManageProperties.API.Middlewares
{
    /// <summary>
    /// Middleware para gestionar y centralizar el manejo de excepciones en la aplicación.
    /// </summary>
    public class ManagerExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="ManagerExceptionsMiddleware"/>.
        /// </summary>
        /// <param name="next">Delegado que representa el siguiente middleware en la tubería de procesamiento.</param>
        public ManagerExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método de ejecución del middleware. 
        /// Intercepta las solicitudes HTTP y maneja las excepciones que se produzcan durante su procesamiento.
        /// </summary>
        /// <param name="context">El contexto HTTP de la solicitud actual.</param>
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

        /// <summary>
        /// Maneja las excepciones capturadas y construye una respuesta HTTP adecuada.
        /// </summary>
        /// <param name="context">El contexto HTTP donde se generó la excepción.</param>
        /// <param name="ex">Excepción lanzada durante el procesamiento de la solicitud.</param>
        /// <returns>Tarea asincrónica que escribe la respuesta en formato JSON.</returns>
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

    /// <summary>
    /// Clase de extensión para registrar el <see cref="ManagerExceptionsMiddleware"/> 
    /// en el pipeline de la aplicación.
    /// </summary>
    public static class ManagerExceptionsMiddlewareExtension
    {
        /// <summary>
        /// Método de extensión que agrega el <see cref="ManagerExceptionsMiddleware"/> al pipeline de middleware de la aplicación.
        /// </summary>
        /// <param name="builder">Instancia de <see cref="IApplicationBuilder"/> para configurar la aplicación.</param>
        /// <returns>El mismo <see cref="IApplicationBuilder"/> para permitir la encadenación de métodos.</returns>
        public static IApplicationBuilder UseManagerExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManagerExceptionsMiddleware>();
        }
    }
}
