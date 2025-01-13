using Microsoft.AspNetCore.Identity;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Services
{
    public class AccountService(RestaurantDbContext context, IPasswordHasher<User> passwordHasher) : IAccountService
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

            string hashPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashPassword;
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}