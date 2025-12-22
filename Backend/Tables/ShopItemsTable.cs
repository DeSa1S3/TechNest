using System.ComponentModel.DataAnnotations;

namespace Backend.Tables
{
    public class ShopItemsTable
    {
        [Key]
        public int Id { get; set; }
        public string? Img { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Quantity { get; set; }

        public string Category { get; set; }
    }
}
