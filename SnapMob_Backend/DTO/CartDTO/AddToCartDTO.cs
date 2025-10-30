namespace SnapMob_Backend.DTO.CartDTO
{
    public class AddToCartDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
