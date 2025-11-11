namespace SnapMob_Backend.DTO.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; }

        public string Battery { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        
        public int BrandId { get; set; }

        

        public List<string> ImageUrls { get; set; } = new();
    }
}
