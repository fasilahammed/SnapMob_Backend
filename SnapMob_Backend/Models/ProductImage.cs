namespace SnapMob_Backend.Models
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public int ProductId { get; set; }

        // Navigation
        public Product Product { get; set; } = null!;
        public bool IsMain { get; set; } = false;
    }
}
