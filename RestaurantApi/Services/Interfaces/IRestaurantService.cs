using RestaurantApi.Models;
using System.Security.Claims;

namespace RestaurantApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto, int userId);
        void Detete(int id, ClaimsPrincipal user);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
    }
}