using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantID, CreateDishDto dto);

        List<DishDto> GetAll(int restaurantId);

        DishDto GetById(int restaurantId, int dishId);

        void RemoveAll(int restaurantId);
    }
}