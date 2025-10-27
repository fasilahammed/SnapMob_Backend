using SnapMob_Backend.Models;

namespace SnapMob_Backend.Repositories.interfaces
{
    public interface IProductBrandRepository : IGenericRepository<ProductBrand>
    {
        Task<ProductBrand?> GetByNameAsync(string name);
    }
}
