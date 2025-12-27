using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Tables
{
    public class CatalogTable
    {
        [Key]
        public int Id { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual CatalogTable? ParentCategory { get; set; }
        public virtual ICollection<CatalogTable> SubCategories { get; set; } = new List<CatalogTable>();

    }
}
