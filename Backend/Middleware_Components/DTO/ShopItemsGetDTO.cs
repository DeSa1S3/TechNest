namespace Backend.Middleware_Components.DTO
{
    public class ShopItems_Get
    {
        public int Id { get; set; }

        public string? Img { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Quantity { get; set; }

        public string Category { get; set; }
    }
}