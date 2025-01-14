using Microsoft.AspNetCore.Authorization;

namespace RestaurantApi.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirment(ResourceOperation resourceOperation) : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; } = resourceOperation;
    }
}