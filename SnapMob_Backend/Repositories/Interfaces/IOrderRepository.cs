using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using System.Threading.Tasks;

namespace SnapMob_Backend.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderWithItemsByIdAsync(int orderId, int userId);
    }
}
