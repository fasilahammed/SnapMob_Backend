using SnapMob_Backend.Common;

public interface ICartService
{
    Task<ApiResponse<object>> GetCartForUserAsync(int userId);
    Task<ApiResponse<string>> AddToCartAsync(int userId, int productId, int quantity);
    Task<ApiResponse<string>> UpdateCartItemAsync(int userId, int cartItemId, int quantity);
    Task<ApiResponse<string>> RemoveCartItemAsync(int userId, int cartItemId);
    Task<ApiResponse<string>> ClearCartAsync(int userId);
}
