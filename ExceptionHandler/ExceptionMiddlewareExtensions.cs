using AlumniProject.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.ExceptionHandler
{

    public class ExceptionMiddlewareExtensions
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddlewareExtensions(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = String.Empty;
            string message;

            var exceptionType = exception.GetType();
            if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(ConflictException))
            {
                message = exception.Message;
                status = HttpStatusCode.Conflict;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message,
                stackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
