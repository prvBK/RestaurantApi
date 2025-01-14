using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantApi.Authorization
{
    public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirement> logger) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            DateTime dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            string userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                logger.LogInformation($"User {userEmail} with date of birth: [{dateOfBirth}] - authorization succeded");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogInformation($"User {userEmail} with date of birth: [{dateOfBirth}] - authorization faild");
            }

            return Task.CompletedTask;
        }
    }
}