using RestaurantApi.Entities;
using RestaurantApi.Exceptions;

namespace RestaurantApi.Helpers
{
    public class RestaurantHelper()
    {
        public static Restaurant GetRestaurantById(RestaurantDbContext context, int restaurantId)
        {
            Restaurant restaurant = context.Restaurants.FirstOrDefault(r => r.Id == restaurantId) ?? throw new NotFoundException("Restaurant not found");

            return restaurant;
        }
    }
}