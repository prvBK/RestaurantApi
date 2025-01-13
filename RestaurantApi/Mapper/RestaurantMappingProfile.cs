﻿using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi.Mapper
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<Address, AddressDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                c => c.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street
                }));

            CreateMap<CreateDishDto, Dish>();

        }
    }
}