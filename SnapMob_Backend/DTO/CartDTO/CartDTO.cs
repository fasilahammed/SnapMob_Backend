namespace SnapMob_Backend.DTO.CartDTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDTO>? Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
