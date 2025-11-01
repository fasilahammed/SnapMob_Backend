using SnapMob_Backend.Common;
using System.Text.Json;

namespace SnapMob_Backend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled Exception: {ex.Message}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>(
                statusCode: StatusCodes.Status500InternalServerError,
                message: "An unexpected error occurred. Please try again later."
                 );
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);

            }
        }
    }
    }
