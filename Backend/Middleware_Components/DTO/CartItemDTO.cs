namespace Backend.Middleware_Components.DTO
{
    public class CartItemDTO
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public float Price {  get; set; }
        public string? Img { get; set; }
    }
}