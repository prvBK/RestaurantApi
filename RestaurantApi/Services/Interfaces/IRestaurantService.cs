using RestaurantApi.Models;

namespace RestaurantApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        void Detete(int id);
        PageResult<RestaurantDto> GetAll(RestaurantQuery query);
        RestaurantDto GetById(int id);
        void Update(int id, UpdateRestaurantDto dto);

    }
}