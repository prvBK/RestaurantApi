using RestaurantApi.Exceptions;

namespace RestaurantApi.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
                //context.Response.Headers.Append("ErrorMessage", notFoundException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}