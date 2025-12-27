namespace Middleware_Components.DTO
{
    public class AuthTokenInfo
    {
        public string? accessToken { get; set; }
        public string? refreshToken { get; set; }
        public DateTime expires_at { get; set; }
        public string? error { get; set; }
    }
}