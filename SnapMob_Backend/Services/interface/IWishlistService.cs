using SnapMob_Backend.Common;
using SnapMob_Backend.DTO;

namespace SnapMob_Backend.Services.Services.interfaces
{
    public interface IWishlistService
    {
        Task<ApiResponse<IEnumerable<WishlistDTO>>> GetWishlistAsync(int userId);
        Task<ApiResponse<string>> ToggleWishlistAsync(int userId, int productId);
    }
}
