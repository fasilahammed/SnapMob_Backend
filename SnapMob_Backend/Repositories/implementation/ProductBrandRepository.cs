using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Data;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Repositories.Implementation;


namespace SnapMob_Backend.Repositories.implementation
{
    public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
    {
        private readonly AppDbContext _context;

        public ProductBrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductBrand?> GetByNameAsync(string name)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower() && !b.IsDeleted);
        }
    }
}
