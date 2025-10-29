namespace SnapMob_Backend.Models
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }

        // 🧭 Navigation
        public User? User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
