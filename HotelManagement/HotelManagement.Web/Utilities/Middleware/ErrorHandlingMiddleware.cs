using HotelManagement.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Utilities.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);

                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/error/pagenotfound");
                }
            }
            catch (EntityInvalidException ex)
            {
                context.Response.Redirect($"/error/invalid?error={ex.Message}");
            }
            catch (Exception)
            {
                context.Response.Redirect("/error/servererror");
            }
        }
    }
}
