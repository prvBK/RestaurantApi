﻿using RestaurantApi.Models.Enum;

namespace RestaurantApi.Models
{
    public class RestaurantQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchPhrase { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}