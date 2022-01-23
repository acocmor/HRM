using HRM.API.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HRM.Core.Extensions
{
    public static class AppExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}