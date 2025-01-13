using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;

namespace RestaurantApi.HelpersAndExtensions
{
    public class RestaurantHelper
    {
        public static Restaurant GetRestaurantById(RestaurantDbContext context, int restaurantId)
        {
            Restaurant restaurant = context.Restaurants.FirstOrDefault(r => r.Id == restaurantId) ?? throw new NotFoundException("Restaurant not found");

            return restaurant;
        }

        public static Restaurant GetRestaurantByIdWithDishes(RestaurantDbContext context, int restaurantId)
        {
            Restaurant restaurant = context.
                Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId) ?? throw new NotFoundException("Restaurant not found");

            return restaurant;
        }

        public static Restaurant GetRestaurantByIdWithDishesAndAdress(RestaurantDbContext context, int restaurantId)
        {
            Restaurant restaurant = context.
                Restaurants
                .Include(r => r.Dishes)
                .Include(r => r.Address)
                .FirstOrDefault(r => r.Id == restaurantId) ?? throw new NotFoundException("Restaurant not found");

            return restaurant;
        }
    }
}