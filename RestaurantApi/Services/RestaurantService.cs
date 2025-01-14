using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Authorization;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.HelpersAndExtensions;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;
using System.Security.Claims;

namespace RestaurantApi.Services
{
    public class RestaurantService(RestaurantDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService) : IRestaurantService
    {
        public int Create(CreateRestaurantDto dto, int userId)
        {
            Restaurant restaurant = mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userId;
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();
            return restaurant.Id;
        }

        public void Detete(int id, ClaimsPrincipal user)
        {

            Restaurant? restaurant = RestaurantHelper.GetRestaurantById(dbContext, id);

            AuthorizationResult authorizationResult = authorizationService.AuthorizeAsync(user, restaurant, new ResourceOperationRequirment(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            dbContext.Restaurants.Remove(restaurant);
            dbContext.SaveChanges();
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            List<Restaurant> restaurants = [.. dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)];

            List<RestaurantDto> restaurantDto = mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantDto;
        }

        public RestaurantDto GetById(int id)
        {
            Restaurant? restaurant = RestaurantHelper.GetRestaurantByIdWithDishesAndAdress(dbContext, id);
            RestaurantDto restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user)
        {
            Restaurant? restaurant = RestaurantHelper.GetRestaurantById(dbContext, id);

            AuthorizationResult authorizationResult = authorizationService.AuthorizeAsync(user, restaurant, new ResourceOperationRequirment(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            dbContext.SaveChanges();
        }
    }
}