namespace RestaurantApi.Authentication
{
    public class AuthenticationSettings
    {
        public string? JwtKey { get; set; }
        public int? JwtExpireDay { get; set; }
        public string? JwtIssuer { get; set; }
    }
}
