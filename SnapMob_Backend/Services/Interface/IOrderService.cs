using SnapMob_Backend.Common;
using SnapMob_Backend.DTOs.OrderDTOs;
using SnapMob_Backend.Enums;

namespace SnapMob_Backend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderDto>> CreateOrderAsync(int userId, CreateOrderDto dto);
        Task<ApiResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(int userId);
        Task<ApiResponse<OrderDto>> GetOrderByIdAsync(int userId, int orderId);
        Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
        Task<ApiResponse<string>> CancelOrderAsync(int orderId);
        Task<ApiResponse<IEnumerable<OrderDto>>> FilterOrdersAsync(string status, string? search);
        Task<ApiResponse<object>> GetDashboardStatsAsync(string type);
    }
}
