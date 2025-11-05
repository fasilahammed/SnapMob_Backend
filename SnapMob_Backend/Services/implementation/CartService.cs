using SnapMob_Backend.Common;
using SnapMob_Backend.Repositories.Interfaces;


public class CartService : ICartService
{
    private readonly IProductRepository _productRepo;
    private readonly ICartRepository _cartRepo;

    public CartService(IProductRepository productRepo, ICartRepository cartRepo)
    {
        _productRepo = productRepo;
        _cartRepo = cartRepo;
    }

    public async Task<ApiResponse<string>> AddToCartAsync(int userId, int productId, int quantity)
    {
        if (quantity < 1 || quantity > 5)
            return new ApiResponse<string>(400, "Quantity must be between 1 and 5");

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

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            int newTotalQty = existingItem.Quantity + quantity;
            if (newTotalQty > product.CurrentStock)
                return new ApiResponse<string>(400, $"Only {product.CurrentStock} items available in stock");

            if (newTotalQty > 5)
                return new ApiResponse<string>(400, "Quantity cannot exceed 5");

            existingItem.Quantity = newTotalQty;
        }
        else
        {
            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                ImageUrl = product.Images?.FirstOrDefault()?.ImageUrl,
                Quantity = quantity
            };
            cart.Items.Add(cartItem);
        }

        await _cartRepo.SaveChangesAsync();
        return new ApiResponse<string>(200, "Product added to cart successfully");
    }

    public async Task<ApiResponse<object>> GetCartForUserAsync(int userId)
    {
        var cart = await _cartRepo.GetCartWithItemsByUserIdAsync(userId);

        if (cart == null || !cart.Items.Any())
            return new ApiResponse<object>(200, "Cart is empty", new
            {
                TotalItems = 0,
                TotalAmount = 0m,
                Items = Array.Empty<object>()
            });

        var items = cart.Items.Select(i => new
        {
            i.Id,
            i.ProductId,
            i.ProductName,
            i.Price,
            i.Quantity,
            i.ImageUrl,
            SubTotal = i.Price * i.Quantity 
        }).ToList();

        var totalAmount = items.Sum(i => i.SubTotal);

        var response = new
        {
            TotalItems = items.Sum(i => i.Quantity),
            TotalAmount = totalAmount,
            Items = items
        };

        return new ApiResponse<object>(200, "Cart fetched successfully", response);
    }


    public async Task<ApiResponse<string>> UpdateCartItemAsync(int userId, int cartItemId, int quantity)
    {
        if (quantity < 1 || quantity > 5)
            return new ApiResponse<string>(400, "Quantity must be between 1 and 5");

        var cartItem = await _cartRepo.GetCartItemByIdAsync(cartItemId, userId);
        if (cartItem == null)
            return new ApiResponse<string>(404, "Cart item not found");

        cartItem.Quantity = quantity;
        _cartRepo.Update(cartItem);
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
