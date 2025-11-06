using SnapMob_Backend.Models;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
   
}
