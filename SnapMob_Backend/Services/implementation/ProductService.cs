using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Services.implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly CloudinaryService _cloudinaryService;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            CloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ProductListResponseDTO> GetProductsAsync(ProductQueryDTO query)
        {
            var products = await _productRepository.GetProductsAsync(
                query.Search,
                query.BrandId,
                query.MinPrice,
                query.MaxPrice,
                query.Page,
                query.PageSize
            );

            var totalCount = await _productRepository.GetProductsCountAsync(
                query.Search,
                query.BrandId,
                query.MinPrice,
                query.MaxPrice
            );

            var productDtos = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return new ProductListResponseDTO
            {
                Products = productDtos,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return _mapper.Map<ProductDTO>(product);
        }


        public async Task<ProductDTO> AddProductAsync(ProductCreateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.Images = new List<ProductImage>();

            if (dto.Images != null && dto.Images.Any())
            {
                foreach (var file in dto.Images)
                {
                    var upload = await _cloudinaryService.UploadImageAsync(file);

                    product.Images.Add(new ProductImage
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        IsMain = product.Images.Count == 0
                    });
                }
            }

            await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO?> UpdateProductAsync(int id, ProductUpdateDTO dto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return null;

            _mapper.Map(dto, existingProduct);

            if (dto.Images != null && dto.Images.Any())
            {
                foreach (var img in existingProduct.Images)
                {
                    if (!string.IsNullOrEmpty(img.PublicId))
                        await _cloudinaryService.DeleteImageAsync(img.PublicId);
                }

                existingProduct.Images.Clear();

                foreach (var file in dto.Images)
                {
                    var upload = await _cloudinaryService.UploadImageAsync(file);

                    existingProduct.Images.Add(new ProductImage
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        IsMain = existingProduct.Images.Count == 0
                    });
                }
            }

            await _productRepository.UpdateAsync(existingProduct);
            return _mapper.Map<ProductDTO>(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            foreach (var img in product.Images)
            {
                if (!string.IsNullOrEmpty(img.PublicId))
                    await _cloudinaryService.DeleteImageAsync(img.PublicId);
            }

            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
