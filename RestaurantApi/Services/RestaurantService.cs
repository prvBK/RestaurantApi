using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public class RestaurantService(RestaurantDbContext dbContext, IMapper mapper) : IRestaurantService
    {
        public RestaurantDto GetById(int id)
        {
            Restaurant? restaurant = dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id) ?? throw new NotFoundException("Restaurant not found");
            RestaurantDto restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
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

        public int Create(CreateRestaurantDto dto)
        {
            Restaurant restaurant = mapper.Map<Restaurant>(dto);
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();
            return restaurant.Id;
        }

        public void Detete(int id)
        {
            Restaurant? restaurant = dbContext.Restaurants.SingleOrDefault(r => r.Id == id) ?? throw new NotFoundException("Restaurant not found");
            dbContext.Restaurants.Remove(restaurant);
            dbContext.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            Restaurant? restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id) ?? throw new NotFoundException("Restaurant not found");
            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            dbContext.SaveChanges();
        }
    }
}