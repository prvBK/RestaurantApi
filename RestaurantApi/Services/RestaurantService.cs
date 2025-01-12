using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private RestaurantDbContext _dbContext;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public RestaurantDto GetById(int id)
        {
            Restaurant? restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return null;
            }

            RestaurantDto restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            List<Restaurant> restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            List<RestaurantDto> restaurantDto = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantDto;
        }

        public int Create(CreateRestaurantDto dto)
        {
            Restaurant restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
    }
}