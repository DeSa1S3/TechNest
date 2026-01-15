namespace Backend.Middleware_Components.DTO
{
    public class UserChangeDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
    }
}