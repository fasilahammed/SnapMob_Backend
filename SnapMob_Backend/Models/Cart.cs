using SnapMob_Backend.Models;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public List<CartItem> Items { get; set; } = new List<CartItem>();
   
}
