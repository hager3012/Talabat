using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                 await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var respons = _environment.IsDevelopment() ?
                    new ApiExceptionResone((int)HttpStatusCode.InternalServerError, ex.Message,ex.StackTrace.ToString())
                    : new ApiExceptionResone((int)HttpStatusCode.InternalServerError);
                var options=new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var JsonRespons=JsonSerializer.Serialize(respons, options);
                await context.Response.WriteAsync(JsonRespons);
            }
        }
    }
}
