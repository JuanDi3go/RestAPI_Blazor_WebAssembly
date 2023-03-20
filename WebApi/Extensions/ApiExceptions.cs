using WebApi.Middleware;

namespace WebApi.Extensions
{
    public static class ApiExceptions
    {
        public static void ErrorHandligMiddleware(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ErroHandlerMiddleware>();
        }
    }
}
