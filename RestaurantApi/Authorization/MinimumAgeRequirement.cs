using Microsoft.AspNetCore.Authorization;

namespace RestaurantApi.Authorization
{
    public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}