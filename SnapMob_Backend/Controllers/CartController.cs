using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTO.CartDTO;
using SnapMob_Backend.Services.interfaces;

namespace SnapMob_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var result = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(new ApiResponse<CartDTO>(200, "Cart fetched successfully", result));
        }

        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddItem(int userId, int productId, int quantity)
        {
            var result = await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok(new ApiResponse<CartDTO>(200, "Item added to cart", result));
        }

        [HttpDelete("{userId}/remove/{productId}")]
        public async Task<IActionResult> RemoveItem(int userId, int productId)
        {
            var success = await _cartService.RemoveFromCartAsync(userId, productId);
            return success
                ? Ok(new ApiResponse<string>(200, "Item removed successfully"))
                : NotFound(new ApiResponse<string>(404, "Item not found"));
        }

        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var success = await _cartService.ClearCartAsync(userId);
            return success
                ? Ok(new ApiResponse<string>(200, "Cart cleared successfully"))
                : NotFound(new ApiResponse<string>(404, "Cart not found"));
        }
    }
}
