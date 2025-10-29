using AutoMapper;
using SnapMob_Backend.DTO.CartDTO;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Services.interfaces;

namespace SnapMob_Backend.Services.implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepo, IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepo.GetCartWithItemsAsync(userId);
            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<CartDTO> AddToCartAsync(int userId, int productId, int quantity)
        {
            // 🧩 Quantity validation
            if (quantity < 1)
                quantity = 1;
            else if (quantity > 5)
                quantity = 5;

            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) throw new Exception("Product not found");

            var cart = await _cartRepo.GetCartWithItemsAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _cartRepo.AddAsync(cart);
            }

            var existingItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

                // ✅ Enforce max per item
                if (existingItem.Quantity > 5)
                    existingItem.Quantity = 5;
            }
            else
            {
                // ✅ Enforce total items count limit (optional)
                if (cart.CartItems!.Count >= 5)
                    throw new Exception("Maximum 5 products allowed in the cart.");

                cart.CartItems!.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    PriceAtAddTime = product.Price
                });
            }

            await _cartRepo.UpdateAsync(cart);
            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _cartRepo.GetCartWithItemsAsync(userId);
            if (cart == null) return false;

            var item = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null) return false;

            cart.CartItems!.Remove(item);
            await _cartRepo.UpdateAsync(cart);
            return true;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _cartRepo.GetCartWithItemsAsync(userId);
            if (cart == null) return false;

            cart.CartItems?.Clear();
            await _cartRepo.UpdateAsync(cart);
            return true;
        }
    }
}
