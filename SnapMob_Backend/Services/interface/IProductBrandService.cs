using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.DTOs;

namespace SnapMob_Backend.Services.Services.interfaces
{
    public interface IProductBrandService
    {
        Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync();
        Task<ProductBrandDTO?> GetBrandByIdAsync(int id);
        Task<ProductBrandDTO> CreateBrandAsync(ProductBrandDTO dto);
        Task<ProductBrandDTO?> UpdateBrandAsync(int id, ProductBrandDTO dto);
        Task<bool> DeleteBrandAsync(int id);
    }
}
