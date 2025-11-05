using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Data;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.Implementation;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Cart?> GetCartWithItemsByUserIdAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

        if (cart == null)
            return null;

        cart.Items = cart.Items
            .Where(i => i.Product != null && !i.Product.IsDeleted && i.Product.IsActive)
            .ToList();

        foreach (var item in cart.Items)
        {
            if (string.IsNullOrEmpty(item.ImageUrl))
            {
                item.ImageUrl = item.Product?.Images.FirstOrDefault(img => img.IsMain)?.ImageUrl
                    ?? item.Product?.Images.FirstOrDefault()?.ImageUrl
                    ?? "https://via.placeholder.com/150"; 
            }
        }

        return cart;
    }



    public async Task<CartItem?> GetCartItemByIdAsync(int cartItemId, int userId)
    {
        return await _context.CartItems
            .Include(ci => ci.Cart)
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.Cart.UserId == userId && !ci.Cart.IsDeleted);
    }

    public void Update(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
    }

    public async Task ClearCartForUserAsync(int userId)
    {
        var cart = await GetCartWithItemsByUserIdAsync(userId);
        if (cart != null && cart.Items.Any())
        {
            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();
        }
    }
}
