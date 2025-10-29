using SnapMob_Backend.DTO.CartDTO;

namespace SnapMob_Backend.Services.interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetCartByUserIdAsync(int userId);
        Task<CartDTO> AddToCartAsync(int userId, int productId, int quantity);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
        Task<bool> ClearCartAsync(int userId);
    }
}
