namespace SnapMob_Backend.DTO.ProductDTO
{
    public class ProductQueryDTO
    {
        public string? Search { get; set; }
        public int? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
