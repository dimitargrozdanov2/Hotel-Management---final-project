using HotelManagement.Web.Utilities.Middleware;
using Microsoft.AspNetCore.Builder;

namespace HotelManagement.Web.Utilities
{
    public static class AppBuilderExtension
    {
        public static void CustomExceptionHandling(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}