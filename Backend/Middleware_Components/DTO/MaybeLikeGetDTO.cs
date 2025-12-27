namespace Backend.Middleware_Components.DTO
{
    public class MaybeLikeGetDTO
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? FinalPrice => Discount.HasValue ? Price * (1 - Discount.Value / 100) : Price;
        public string? Category { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
