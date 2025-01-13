using RestaurantApi.Entities;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Services
{
    public class AccountService(RestaurantDbContext context) : IAccountService
    {
        public void RegisterUser(RegisterUserDto dto)
        {
            User newUser = new()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId ?? context.Roles.Select(r => r.Id).FirstOrDefault()
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}
