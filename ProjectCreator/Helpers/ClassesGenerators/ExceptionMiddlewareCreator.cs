using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCreator.Helpers.ClassesGenerators
{
    public class ExceptionMiddlewareCreator
    {
        public static string GetMiddlewareConfiguration(string projectName, bool isSinglelayer = true)
        {
            var import = isSinglelayer ? projectName : projectName.Replace(".API", "");
            return @$"using {import}.Models.Contracts;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;

namespace {projectName}.Middlewares;

public class ExceptionHandlerMiddleware
{{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {{
        _next = next;
        _logger = logger;
    }}

    public async Task Invoke(HttpContext context)
    {{
        try
        {{
            await _next(context);
        }}
        catch (Exception ex)
        {{
            await HandleExceptionAsync(context, ex);
        }}
    }}

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {{
        context.Response.ContentType = ""application/json"";
        _ = context.Request.Headers.TryGetValue(""CorrelationId"", out StringValues correlationId);

        _logger.LogError(exception, $""Exception Occured in Middleware Tracking id {{correlationId}}"");

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new BaseResponse();
        errorResponse.Message = ""we are unable to process your request at this time"";
        errorResponse.CorrelationId = correlationId;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }}
}}";
        }
    }
}
