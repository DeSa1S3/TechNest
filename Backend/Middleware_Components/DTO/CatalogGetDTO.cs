namespace Backend.Middleware_Components.DTO
{
    public class CatalogGetDTO
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public List<CatalogGetDTO> SubCategories { get; set; } = new List<CatalogGetDTO>();

    }
}
