using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;

public interface ICartRepository : IGenericRepository<Cart>
{
    Task<Cart?> GetCartWithItemsByUserIdAsync(int userId);
    Task<CartItem?> GetCartItemByIdAsync(int cartItemId, int userId);
    void Update(CartItem cartItem);
    Task ClearCartForUserAsync(int userId);
}
