namespace RestaurantApi.Models
{
    public class DishDto
    {
        public string? Description { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}