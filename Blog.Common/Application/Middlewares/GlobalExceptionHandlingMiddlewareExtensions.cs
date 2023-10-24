using Microsoft.AspNetCore.Builder;

namespace Blog.Common.Application.Middlewares
{
    public static class GlobalExceptionHandlingMiddlewareExtensions
    {
        public static WebApplication UseGlobalExceptionHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }
    }
}
