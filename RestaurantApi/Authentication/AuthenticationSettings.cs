namespace RestaurantApi.Authentication
{
    public class AuthenticationSettings
    {
        public string? JwtKey { get; set; }
        public int? JwtExpireDay { get; set; }
        public string? JwtIssuer { get; set; }
        public string? TokenToTesting { get; set; }

        public string? TokenToTestingDescription { get; set; }
    }
}