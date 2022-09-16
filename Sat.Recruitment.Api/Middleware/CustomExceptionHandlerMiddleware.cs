using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Sat.Recruitment.Common.Execptions;
using System;

using System.Net;
using System.Threading.Tasks;
namespace Sat.Recruitment.Api.Middleware
{
    public class CustomExceptionHandlerMiddleware : ExceptionFilterAttribute
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            string result = string.Empty;

            switch (exception)
            {
                //case ValidationException validationException:
                //    code = HttpStatusCode.BadRequest;
                //    result = JsonConvert.SerializeObject(validationException.Failures);
                //    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
