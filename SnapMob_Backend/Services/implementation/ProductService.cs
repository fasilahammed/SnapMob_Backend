using AutoMapper;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Services.implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductListResponseDTO> GetProductsAsync(ProductQueryDTO query)
        {
            var products = await _productRepository.GetProductsAsync(
                search: query.Search,
                brandId: query.BrandId,
                minPrice: query.MinPrice,
                maxPrice: query.MaxPrice,
                page: query.Page,
                pageSize: query.PageSize
            );

            var totalCount = await _productRepository.GetProductsCountAsync(
                search: query.Search,
                brandId: query.BrandId,
                minPrice: query.MinPrice,
                maxPrice: query.MaxPrice
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
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}