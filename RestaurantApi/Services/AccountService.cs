using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Authentication;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantApi.Services
{
    public class AccountService(RestaurantDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings) : IAccountService
    {
        public string GenerateJwt(LoginDto dto)
        {
            User? user = context
                .Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || user.PasswordHash == null)
            {
                throw new BadRequestEcetpion("Invalid username or password");
            }

            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestEcetpion("Invalid username or password");
            }

            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty),
                new Claim("DateOfBirth", user.DateOfBirth is not null ? user.DateOfBirth.Value.ToString("yyyy-MM-dd") : "yyyy-MM-dd")
            ];

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(new Claim("Nationality", user.Nationality));
            }

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey ?? string.Empty));
            SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDay ?? 1);

            JwtSecurityToken token = new(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            JwtSecurityTokenHandler tokenHandler = new();
            return tokenHandler.WriteToken(token);
        }

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