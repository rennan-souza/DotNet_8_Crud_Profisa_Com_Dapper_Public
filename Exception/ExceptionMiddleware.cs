using System.Net;
using System.Text.Json;

namespace CrudProfisaComDapper.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex) // Captura exceções personalizadas
            {
                await HandleCustomExceptionAsync(context, ex);
            }
            catch (IOException ex) // Captura outras exceções
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private static Task HandleCustomExceptionAsync(HttpContext context, CustomException ex)
        {
            var response = new
            {
                message = ex.Message,
                statusCode = ex.StatusCode
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static Task HandleGenericExceptionAsync(HttpContext context, IOException ex)
        {
            var response = new
            {
                message = "Erro interno no servidor.",
                details = ex.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
