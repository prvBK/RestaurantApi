using RestaurantApi.Services.Interfaces;
using System.Security.Claims;

namespace RestaurantApi.Services
{
    public class UserContextService(IHttpContextAccessor httpContextAccesor) : IUserContextService
    {
        public ClaimsPrincipal User => httpContextAccesor.HttpContext?.User;
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

    }
}
