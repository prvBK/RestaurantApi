using Microsoft.AspNetCore.Authorization;
using RestaurantApi.Entities;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Authorization
{
    public class MinimumNRestaurantCreatedHandler(RestaurantDbContext restaurantDbContext, IUserContextService userContextService) : AuthorizationHandler<MinimumNRestaurantCreated>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumNRestaurantCreated requirement)
        {
            int? userID = userContextService.GetUserId;
            int crateRestaurantCount = restaurantDbContext.Restaurants.Count(r => r.CreatedById == userID);

            if (crateRestaurantCount >= requirement.MinimumRestaurantCreate)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}