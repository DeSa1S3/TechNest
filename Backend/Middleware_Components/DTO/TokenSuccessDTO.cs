namespace Backend.Middleware_Components.DTO
{
    public class TokenSuccessDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public List<string>? Roles { get; set; }
    }
}
