using AutoMapper;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTO.CartDTO;
using SnapMob_Backend.Repositories.Interfaces;

public class CartService : ICartService
{
    private readonly IProductRepository _productRepo;
    private readonly ICartRepository _cartRepo;
    private readonly IMapper _mapper;

    public CartService(IProductRepository productRepo, ICartRepository cartRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _cartRepo = cartRepo;
        _mapper = mapper;
    }

    public async Task<ApiResponse<string>> AddToCartAsync(int userId, int productId, int quantity)
    {
        if (quantity < 1 || quantity > 6)
            return new ApiResponse<string>(400, "Quantity must be between 1 and 6");

        var product = await _productRepo.GetByIdAsync(productId);
        if (product == null || product.IsDeleted || !product.IsActive)
            return new ApiResponse<string>(404, "Product not found or inactive");

        if (product.CurrentStock <= 0)
            return new ApiResponse<string>(400, "Product is out of stock");

        if (quantity > product.CurrentStock)
            return new ApiResponse<string>(400, $"Only {product.CurrentStock} items available in stock");

        var cart = await _cartRepo.GetCartWithItemsByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _cartRepo.AddAsync(cart);
        }

        // ✅ Check if product already exists in cart
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            // 🛑 Prevent duplicate addition
            return new ApiResponse<string>(400, "Product already exists in your cart. Update quantity instead.");
        }

        // ✅ Add new item
        var brandName = product.Brand?.Name ?? "Unknown Brand";
        var imageUrl = product.Images?.FirstOrDefault(i => i.IsMain)?.ImageUrl
                       ?? product.Images?.FirstOrDefault()?.ImageUrl
                       ?? "https://via.placeholder.com/150";

        var cartItem = new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            BrandName = brandName,
            Price = product.Price,
            ImageUrl = imageUrl,
            Quantity = quantity
        };

        cart.Items.Add(cartItem);
        await _cartRepo.SaveChangesAsync();

        return new ApiResponse<string>(200, "Product added to cart successfully");
    }


    public async Task<ApiResponse<CartDto>> GetCartForUserAsync(int userId)
    {
        var cart = await _cartRepo.GetCartWithItemsByUserIdAsync(userId);
        if (cart == null || !cart.Items.Any())
            return new ApiResponse<CartDto>(200, "Cart is empty", new CartDto());

        var cartDto = _mapper.Map<CartDto>(cart);
        return new ApiResponse<CartDto>(200, "Cart fetched successfully", cartDto);
    }

    public async Task<ApiResponse<string>> UpdateCartItemAsync(int userId, int cartItemId, int quantity)
    {
        if (quantity < 1 || quantity > 5)
            return new ApiResponse<string>(400, "Quantity must be between 1 and 5");

        var item = await _cartRepo.GetCartItemByIdAsync(cartItemId, userId);
        if (item == null)
            return new ApiResponse<string>(404, "Cart item not found");

        item.Quantity = quantity;
        _cartRepo.Update(item);
        await _cartRepo.SaveChangesAsync();

        return new ApiResponse<string>(200, "Quantity updated successfully");
    }

    public async Task<ApiResponse<string>> RemoveCartItemAsync(int userId, int cartItemId)
    {
        var cart = await _cartRepo.GetCartWithItemsByUserIdAsync(userId);
        if (cart == null) return new ApiResponse<string>(404, "Cart not found");

        var item = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
        if (item == null) return new ApiResponse<string>(404, "Item not found");

        cart.Items.Remove(item);
        await _cartRepo.SaveChangesAsync();

        return new ApiResponse<string>(200, "Item removed successfully");
    }

    public async Task<ApiResponse<string>> ClearCartAsync(int userId)
    {
        await _cartRepo.ClearCartForUserAsync(userId);
        return new ApiResponse<string>(200, "Cart cleared successfully");
    }
}
 