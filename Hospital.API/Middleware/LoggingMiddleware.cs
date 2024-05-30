using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Hospital.API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var stopWatch = Stopwatch.StartNew();

            _logger.LogInformation("Handling request: {Method} {Path}", request.Method, request.Path);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while processing the request.");
                throw; // Re-throw the exception after logging it
            }
            finally
            {
                stopWatch.Stop();
                var response = context.Response;

                _logger.LogInformation("Finished handling request: {Method} {Path} with status code {StatusCode} in {ElapsedMilliseconds}ms",
                    request.Method, request.Path, response.StatusCode, stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
