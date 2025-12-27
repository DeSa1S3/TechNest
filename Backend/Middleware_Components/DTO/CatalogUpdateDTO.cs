namespace Backend.Middleware_Components.DTO
{
    public class CatalogUpdateDTO
    {
        public string? Img { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
