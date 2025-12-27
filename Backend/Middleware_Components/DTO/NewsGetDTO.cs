namespace Backend.Middleware_Components.DTO
{
    public class NewsGetDTO
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
