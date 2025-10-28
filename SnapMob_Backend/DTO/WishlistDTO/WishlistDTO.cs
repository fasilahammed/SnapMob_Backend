namespace SnapMob_Backend.DTO
{
    public class WishlistDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
