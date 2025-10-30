using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapMob_Backend.Common;
using SnapMob_Backend.Services.Services.interfaces;
using System.Security.Claims;

namespace SnapMob_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "User")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID not found in token.");
            return int.Parse(claim.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var userId = GetUserId();
            var response = await _wishlistService.GetWishlistAsync(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> ToggleWishlist(int productId)
        {
            var userId = GetUserId();
            var response = await _wishlistService.ToggleWishlistAsync(userId, productId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
