using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.HelpersAndExtensions;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Services
{
    public class DishService(RestaurantDbContext context, IMapper mapper) : IDishService
    {
        private readonly RestaurantDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public int Create(int restaurantId, CreateDishDto dto)
        {
            RestaurantHelper.GetRestaurantById(_context, restaurantId);

            Dish dishEntitie = _mapper.Map<Dish>(dto);
            dishEntitie.RestaurantId = restaurantId;

            _context.Dishes.Add(dishEntitie);
            _context.SaveChanges();

            return dishEntitie.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            RestaurantHelper.GetRestaurantById(_context, restaurantId);
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.RestaurantId == restaurantId && d.Id == dishId) ?? throw new NotFoundException("Dish not found in current restaurant");
            DishDto dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            RestaurantHelper.GetRestaurantById(_context, restaurantId);

            List<Dish> dishes = [.. _context.Dishes.Where(d => d.RestaurantId == restaurantId)];
            if (dishes.IsNullOrEmpty())
            {
                throw new NotFoundException("Dish not found in current restaurant");
            }

            List<DishDto> dishDtos = _mapper.Map<List<DishDto>>(dishes);

            return dishDtos;
        }

        public void RemoveAll(int restaurantId)
        {
            Restaurant restaurant = RestaurantHelper.GetRestaurantByIdWithDishes(_context, restaurantId);
            if (restaurant.Dishes != null)
            {
                _context.RemoveRange(restaurant.Dishes);
                _context.SaveChanges();
            }
        }
    }
}