using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public interface IDishServive
    {
        int Create(int restaurantID, CreateDishDto dto);
        List<DishDto> GetAll(int restaurantId);
        DishDto GetById(int restaurantId, int dishId);
    }
}