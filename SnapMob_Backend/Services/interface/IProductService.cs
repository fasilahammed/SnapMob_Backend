using SnapMob_Backend.DTO.ProductDTO;

namespace SnapMob_Backend.Services.Services.interfaces
{
    public interface IProductService
    {
        Task<ProductListResponseDTO> GetProductsAsync(ProductQueryDTO query);
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<ProductDTO> AddProductAsync(ProductCreateUpdateDTO dto);
        Task<ProductDTO?> UpdateProductAsync(int id, ProductCreateUpdateDTO dto);
        Task<bool> DeleteProductAsync(int id);
    }
}
