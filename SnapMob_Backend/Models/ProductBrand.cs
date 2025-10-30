namespace SnapMob_Backend.Models
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
