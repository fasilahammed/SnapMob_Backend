using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.Interfaces;


namespace SnapMob_Backend.Repositories.interfaces
{
    public interface IProductBrandRepository : IGenericRepository<ProductBrand>
    {
        Task<ProductBrand?> GetByNameAsync(string name);
    }
}
