﻿namespace RestaurantApi.Models
{
    public class RestaurantDto
    {
        public int AddressId { get; set; }
        public string? Category { get; set; }
        public string? City { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? Description { get; set; }
        public List<DishDto>? Dishes { get; set; }
        public bool HasDelivery { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
    }
}