using AutoMapper;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTO;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Services.Services.implementation
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepo, IProductRepository productRepo, IMapper mapper)
        {
            _wishlistRepo = wishlistRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<WishlistDTO>>> GetWishlistAsync(int userId)
        {
            var items = await _wishlistRepo.GetWishlistByUserAsync(userId);
            var mapped = _mapper.Map<IEnumerable<WishlistDTO>>(items);
            return new ApiResponse<IEnumerable<WishlistDTO>>(200, "Wishlist fetched successfully", mapped);
        }

        public async Task<ApiResponse<string>> ToggleWishlistAsync(int userId, int productId)
        {
            var existing = await _wishlistRepo.GetWishlistItemAsync(userId, productId);

            if (existing != null)
            {
                await _wishlistRepo.RemoveWishlistItemAsync(existing);
                return new ApiResponse<string>(200, "Product removed from wishlist");
            }

            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null || product.IsDeleted)
                return new ApiResponse<string>(404, "Product not found or inactive");

            var wishlist = new Wishlist
            {
                UserId = userId,
                ProductId = productId
            };

            await _wishlistRepo.AddWishlistItemAsync(wishlist);
            return new ApiResponse<string>(200, "Product added to wishlist");
        }
    }
}
