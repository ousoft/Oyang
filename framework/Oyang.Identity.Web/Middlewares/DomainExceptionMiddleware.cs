using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Oyang.Identity.Web.Middlewares
{
    public class DomainExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public DomainExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //当一个请求为AJAX(Http请求头中X - Requested - With为XMLHttpRequest)时.
            //当客户端接受的返回类型为application / json(Http请求头中accept 为application / json)时.
            var xRequestedWith = context.Request.Headers["X-Requested-With"];
            var accept = context.Request.Headers["accept"];
            var isXMLHttpRequest = xRequestedWith.Any(t => t.Contains("XMLHttpRequest"));
            var isApplicationJson = accept.Any(t => t.Contains("application/json"));

            bool isJsonResult = isXMLHttpRequest || isApplicationJson;
            if (isJsonResult)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var result = new Models.ResultModel() { Code = 1 };
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionHandlerPathFeature == null)
                {
                    result.Message = "未知异常";
                }
                else if (exceptionHandlerPathFeature.Error is DomainException domainException)
                {
                    result.Message = domainException.Message;
                    result.ValidationErrors = new List<Models.MemberError>();
                    result.ValidationErrors.Add(new Models.MemberError()
                    {
                        Member = domainException.Member,
                        Message = domainException.Message
                    });
                }
                else
                {
                    result.Message = exceptionHandlerPathFeature.Error.Message;
                }
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(result, options);
                await context.Response.WriteAsync(json);
            }
            else
            {
                context.Response.Redirect("/Home/Error");
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
