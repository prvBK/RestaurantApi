using RestaurantApi.Models;

namespace RestaurantApi.Services.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);

        void RegisterUser(RegisterUserDto dto);
    }
}