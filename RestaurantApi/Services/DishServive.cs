using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public class DishServive(RestaurantDbContext context, IMapper mapper) : IDishServive
    {
        private readonly RestaurantDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public int Create(int restaurantID, CreateDishDto dto)
        {
            Restaurant? restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == restaurantID);
            if (restaurant is not null)
            {
                Dish dishEntitie = _mapper.Map<Dish>(dto);
                dishEntitie.RestaurantId = restaurantID;

                _context.Dishes.Add(dishEntitie);
                _context.SaveChanges();

                return dishEntitie.Id;
            }

            throw new NotFoundException("Restaurant not found");
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            Restaurant? restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == restaurantId) ?? throw new NotFoundException("Restaurant not found");
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.RestaurantId == restaurantId && d.Id == dishId) ?? throw new NotFoundException("Dish not found in current restaurant");
            DishDto dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            Restaurant restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == restaurantId) ?? throw new NotFoundException("Restaurant not found");

            List<Dish> dishes = [.. _context.Dishes.Where(d => d.RestaurantId == restaurantId)];
            if (dishes.IsNullOrEmpty())
            {
                throw new NotFoundException("Dish not found in current restaurant");
            }

            List<DishDto> dishDtos = _mapper.Map<List<DishDto>>(dishes);

            return dishDtos;
        }
    }
}
