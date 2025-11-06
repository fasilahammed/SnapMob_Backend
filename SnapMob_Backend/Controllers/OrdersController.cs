using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTOs.OrderDTOs;
using SnapMob_Backend.Enums;
using SnapMob_Backend.Services.Interfaces;
using System.Security.Claims;

namespace SnapMob_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // ✅ Create order (Customer)
        [HttpPost("checkout")]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim);
            var response = await _orderService.CreateOrderAsync(userId, dto);
            return StatusCode(response.StatusCode, response);
        }

        // ✅ Get logged-in user's orders
        [HttpGet]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> GetMyOrders()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _orderService.GetOrdersByUserIdAsync(userId);
            return StatusCode(response.StatusCode, response);
        }

        // ✅ Get single order details
        [HttpGet("{orderId}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _orderService.GetOrderByIdAsync(userId, orderId);
            return StatusCode(response.StatusCode, response);
        }

        // ✅ Filter orders for admin (All + Search + Status)
        [HttpGet("admin/filter")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> FilterOrders(
            [FromQuery] string status = "all",
            [FromQuery] string? search = null)
        {
            var response = await _orderService.FilterOrdersAsync(status, search);
            return StatusCode(response.StatusCode, response);
        }

        // ✅ Update order status (Admin)
        [HttpPost("admin/update-status/{orderId}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDto dto)
        {
            var response = await _orderService.UpdateOrderStatusAsync(orderId, dto.NewStatus);
            return StatusCode(response.StatusCode, response);
        }

        // ✅ Cancel order (Customer)
        [HttpPost("cancel/{orderId}")]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.CancelOrderAsync(orderId);
            return StatusCode(response.StatusCode, response);
        }

        

        [HttpGet("admin/dashboard")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetDashboardStats([FromQuery] string type = "all")
        {
            var response = await _orderService.GetDashboardStatsAsync(type);
            return StatusCode(response.StatusCode, response);
        }
    }
}
