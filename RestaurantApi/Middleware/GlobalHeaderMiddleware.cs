using RestaurantApi.Authentication;

namespace RestaurantApi.Middleware
{
    public class GlobalHeaderMiddleware(RequestDelegate next, AuthenticationSettings authenticationSettings)
    {
        private readonly AuthenticationSettings _authenticationSettings = authenticationSettings;
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // to testing
            context.Request.Headers.Append("Authorization", _authenticationSettings.TokenToTesting);

            await _next(context);
        }
    }
}