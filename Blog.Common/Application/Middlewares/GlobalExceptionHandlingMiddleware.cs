using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Blog.Common.Application.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Application exception occured {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);

                //minimal api does not support custom model binding...
                if (ex is BadHttpRequestException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var response = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "Validation Error",
                        Detail = ex.Message
                    };
                    string jsonResponse = JsonSerializer.Serialize(response);

                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(jsonResponse);

                    return;
                }

                context.Response.StatusCode = 
                    (int)HttpStatusCode.InternalServerError;
                var problem = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server error has occured"
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
