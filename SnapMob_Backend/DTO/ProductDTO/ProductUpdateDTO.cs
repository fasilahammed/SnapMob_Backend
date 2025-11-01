namespace SnapMob_Backend.DTO.ProductDTO
{
    public class ProductUpdateDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CurrentStock { get; set; }
        public string? Battery { get; set; }
        public string? Camera { get; set; }
        public string? Storage { get; set; }
        public string? Display { get; set; }
        public int? BrandId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
