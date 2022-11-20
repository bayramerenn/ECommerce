using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ECommerceCommon.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(
            RequestDelegate next)

        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        //ArgumentNullException
        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception
        )
        {
            string? result = null;
            switch (exception)
            {
                case RestException re:
                    context.Response.StatusCode = (int)re.Code;
                    result = JsonSerializer.Serialize(new
                    {
                        errors = re.Errors
                    });
                    break;

                case Exception e:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new
                    {
                        errors = Constants.InternalServerError
                    });
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result ?? "{}");
        }
    }
}