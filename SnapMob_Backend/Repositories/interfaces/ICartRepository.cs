using SnapMob_Backend.Models;

namespace SnapMob_Backend.Repositories.interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartWithItemsAsync(int userId);
    }
}
