namespace SnapMob_Backend.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; } = true;

        public string Battery { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;

        public int BrandId { get; set; }

        public ProductBrand Brand { get; set; } = null!;
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
