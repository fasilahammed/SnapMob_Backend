using SnapMob_Backend.Models;

namespace SnapMob_Backend.Repositories.interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(
            string? search = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int page = 1,
            int pageSize = 12);

        Task<int> GetProductsCountAsync(
            string? search = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null);

        Task<bool> ProductExistsAsync(int id);
    }
}
