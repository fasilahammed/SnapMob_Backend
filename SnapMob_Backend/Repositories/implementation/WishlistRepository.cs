using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Data;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;

namespace SnapMob_Backend.Repositories.implementation
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AppDbContext _context;

        public WishlistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Wishlist>> GetWishlistByUserAsync(int userId)
        {
            return await _context.Wishlists
                .Include(w => w.Product)
                    .ThenInclude(p => p.Brand)
                .Include(w => w.Product.Images)
                .Where(w => w.UserId == userId && !w.IsDeleted)
                .ToListAsync();
        }

        public async Task<Wishlist?> GetWishlistItemAsync(int userId, int productId)
        {
            return await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId && !w.IsDeleted);
        }

        public async Task AddWishlistItemAsync(Wishlist wishlist)
        {
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveWishlistItemAsync(Wishlist wishlist)
        {
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }
    }
}
