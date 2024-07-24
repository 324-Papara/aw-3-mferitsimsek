using System.Text;

namespace Para.Api.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Request log 
            LogRequest(context.Request);

            // Capture the response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            // Response log
            LogResponse(context.Response);

            // Response'u kopyalayıp body ye ekliyoruz.
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }

        private void LogRequest(HttpRequest request)
        {
            _logger.LogInformation("Request: {Method} {Scheme}://{Host}{Path}{QueryString}",
                request.Method,
                request.Scheme,
                request.Host,
                request.Path,
                request.QueryString);
        }

        private void LogResponse(HttpResponse response)
        {
            _logger.LogInformation("Response: StatusCode: {StatusCode}", response.StatusCode);
        }
    }
}

