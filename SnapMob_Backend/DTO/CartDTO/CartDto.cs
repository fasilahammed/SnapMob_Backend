using System.Collections.Generic;

namespace SnapMob_Backend.DTO.CartDTO
{
    public class CartDto
    {
        public int TotalItems { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
    }
}
