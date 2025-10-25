namespace SnapMob_Backend.DTO.ProductDTO
{
    public class ProductListResponseDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
