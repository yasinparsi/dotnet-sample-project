using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SnappFood.DotNetSampleProject.App.Shared.Extensions;

public sealed class ExceptionMiddleware
{
    private readonly string _baseMessage = "Something went wrong";
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await _next(context);

            var traceCode = Guid.NewGuid().ToString();
            switch (context.Response.StatusCode)
            {
                case (int)HttpStatusCode.Unauthorized:
                    var unauthorizedResult = GenerateErrorJson("Unauthenticated", 40_001, traceCode);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(unauthorizedResult);
                    break;
                case (int)HttpStatusCode.Forbidden:
                    var forbiddenResult = GenerateErrorJson("Forbidden", 40_001, traceCode);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(forbiddenResult);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            var bmException = ex as SnappFoodException;

            var message = bmException?.Message ?? _baseMessage;
            var traceId = bmException?.TraceId ?? TraceIds.UNKOWN;
            var traceCode = Guid.NewGuid().ToString();
            logger.LogError(ex, "ExceptionMessage: {message}, TraceId: {traceId}, TraceCode: {TraceCode}",
                message, traceId, traceCode);

            var result = GenerateErrorJson(message, traceId, traceCode);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)(bmException?.StatusCode ?? HttpStatusCode.InternalServerError);
            await context.Response.WriteAsync(result);
        }
    }

    private static string GenerateErrorJson(string message, int traceId, string traceCode)
    {
        var apiResult = new ApiError(message, traceId, traceCode);

        JsonSerializerOptions jsonSerializerOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        };
        var result = JsonSerializer.Serialize(apiResult, jsonSerializerOptions);
        return result;
    }
}

class ApiError
{
    public ApiError(string message, int traceId, string traceCode)
    {
        Message = message;
        TraceId = traceId;
        TraceCode = traceCode;
    }

    public string Message { get; private set; } = null!;
    public int TraceId { get; private set; }
    public string TraceCode { get; private set; } = null!;
}