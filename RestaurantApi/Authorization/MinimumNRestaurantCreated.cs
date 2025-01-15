using Microsoft.AspNetCore.Authorization;

namespace RestaurantApi.Authorization
{
    public class MinimumNRestaurantCreated(int n) : IAuthorizationRequirement
    {
        public int MinimumRestaurantCreate { get; } = n;
    }
}