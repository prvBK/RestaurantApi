﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Authorization;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.HelpersAndExtensions;
using RestaurantApi.Models;
using RestaurantApi.Models.Enum;
using RestaurantApi.Services.Interfaces;
using System.Linq.Expressions;

namespace RestaurantApi.Services
{
    public class RestaurantService(RestaurantDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService) : IRestaurantService
    {
        public int Create(CreateRestaurantDto dto)
        {
            Restaurant restaurant = mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userContextService.GetUserId;
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();
            return restaurant.Id;
        }

        public void Detete(int id)
        {
            Restaurant? restaurant = RestaurantHelper.GetRestaurantById(dbContext, id);

            AuthorizationResult authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequirment(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            dbContext.Restaurants.Remove(restaurant);
            dbContext.SaveChanges();
        }

        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            IQueryable<Restaurant> baseQuery = dbContext
               .Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            Dictionary<string, Expression<Func<Restaurant, object>>> columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {  nameof(Restaurant.Name), r => r.Name },
                {  nameof(Restaurant.Description), r => r.Description },
                {  nameof(Restaurant.Category), r => r.Category },
            };

            Expression<Func<Restaurant, object>> selectedColumn = columnsSelector[query.SortBy ?? nameof(Restaurant.Name)];

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            List<Restaurant> restaurants = [.. baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                ];

            List<RestaurantDto> restaurantDto = mapper.Map<List<RestaurantDto>>(restaurants);
            PageResult<RestaurantDto> pageResult = new PageResult<RestaurantDto>(restaurantDto, baseQuery.Count(), query.PageSize, query.PageNumber);

            return pageResult;
        }

        public RestaurantDto GetById(int id)
        {
            Restaurant? restaurant = RestaurantHelper.GetRestaurantByIdWithDishesAndAdress(dbContext, id);
            RestaurantDto restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            Restaurant? restaurant = RestaurantHelper.GetRestaurantById(dbContext, id);

            AuthorizationResult authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequirment(ResourceOperation.Update)).Result;

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