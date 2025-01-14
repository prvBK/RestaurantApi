using Microsoft.AspNetCore.Authorization;
using RestaurantApi.Entities;
using System.Security.Claims;

namespace RestaurantApi.Authorization
{
    public class ResourceOperationRequirmentHandler : AuthorizationHandler<ResourceOperationRequirment, Restaurant>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirment requirement, Restaurant restaurant)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            string userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (restaurant.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}