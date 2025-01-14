using System.Security.Claims;

namespace RestaurantApi.Services.Interfaces
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}