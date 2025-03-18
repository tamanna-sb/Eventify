using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eventify.Backend.UserService.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        // Constructor injection of ILogger to log exception details
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the full exception with stack trace for debugging
                _logger.LogError(ex, ex.Message);
                // Set response status code to InternalServerError
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                // Build a structured error response for the client
                var errorResponse = new
                {
                    Message = ex.Message, // Main exception message
                    ExceptionType = ex.GetType().ToString(), // Type of exception
                    StackTrace = ex.StackTrace, // Full stack trace (can be omitted in production)
                    InnerExceptionMessage = ex.InnerException?.Message // Inner exception message if present
                };

                // Send the error response in JSON format
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}
