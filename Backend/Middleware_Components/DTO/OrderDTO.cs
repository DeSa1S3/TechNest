namespace Backend.Middleware_Components.DTO
{
    public class OrderDTO
    {
        public string OrderId { get; set; }

        public string PickUpStore { get; set; }

        public string ExpectedDateOfReceipt { get; set; }

        public string ShelfLife { get; set; }

        public string PaymentMethod { get; set; }

        public string NameOrder { get; set; }
        
        public int QuantityOrder { get; set; }

        public float PriceOrderId { get; set; }

        public float PriceForAllProducts { get; set; }
    }
}