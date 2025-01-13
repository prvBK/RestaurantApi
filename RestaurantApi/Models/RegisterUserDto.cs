using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class RegisterUserDto
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
        public string? Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}