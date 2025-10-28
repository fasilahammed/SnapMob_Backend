using SnapMob_Backend.Models;

namespace SnapMob_Backend.Repositories.interfaces
{
    public interface IWishlistRepository
    {
        Task<List<Wishlist>> GetWishlistByUserAsync(int userId);
        Task<Wishlist?> GetWishlistItemAsync(int userId, int productId);
        Task AddWishlistItemAsync(Wishlist wishlist);
        Task RemoveWishlistItemAsync(Wishlist wishlist);
    }
}
