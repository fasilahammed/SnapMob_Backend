namespace SnapMob_Backend.DTO.CartDTO
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public decimal PriceAtAddTime { get; set; }
        public int Quantity { get; set; }
    }
}

