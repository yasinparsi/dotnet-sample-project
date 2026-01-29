using System.Net;

namespace SnappFood.DotNetSampleProject.Core.DomainModels.General;

public sealed class SnappFoodException : Exception
{
    public SnappFoodException(HttpStatusCode statusCode, int traceId)
        : base()
    {
        StatusCode = statusCode;
        TraceId = traceId;
    }

    public SnappFoodException(HttpStatusCode statusCode,
        int traceId, string? message)
        : base(message)
    {
        StatusCode = statusCode;
        TraceId = traceId;
    }

    public SnappFoodException(HttpStatusCode statusCode, int traceId,
        string? message, Exception? innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        TraceId = traceId;
    }

    public HttpStatusCode StatusCode { get; private set; }
    public int TraceId { get; private set; }
}
