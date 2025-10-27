using AutoMapper;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.DTOs;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Services.implementation
{
    public class ProductBrandService : IProductBrandService
    {
        private readonly IProductBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public ProductBrandService(IProductBrandRepository brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _brandRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrandDTO>>(brands.Where(b => !b.IsDeleted));
        }

        public async Task<ProductBrandDTO?> GetBrandByIdAsync(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null || brand.IsDeleted) return null;
            return _mapper.Map<ProductBrandDTO>(brand);
        }

        public async Task<ProductBrandDTO> CreateBrandAsync(ProductBrandDTO dto)
        {
            var brand = _mapper.Map<ProductBrand>(dto);
            await _brandRepo.AddAsync(brand);
            return _mapper.Map<ProductBrandDTO>(brand);
        }

        public async Task<ProductBrandDTO?> UpdateBrandAsync(int id, ProductBrandDTO dto)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null || brand.IsDeleted) return null;

            brand.Name = dto.Name;
            brand.ModifiedOn = DateTime.UtcNow;
            await _brandRepo.UpdateAsync(brand);

            return _mapper.Map<ProductBrandDTO>(brand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null || brand.IsDeleted) return false;

            brand.IsDeleted = true;
            brand.DeletedOn = DateTime.UtcNow;
            await _brandRepo.UpdateAsync(brand);

            return true;
        }
    }
}
