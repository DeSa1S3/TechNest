namespace Backend.Middleware_Components.DTO
{
    public class MaybeLikeUpdateDTO
    {
        public string? Img { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsActive { get; set; }
        public int? SortOrder { get; set; }
    }
}
