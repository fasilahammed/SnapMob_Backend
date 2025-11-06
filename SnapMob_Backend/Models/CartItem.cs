using SnapMob_Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int Quantity { get; set; } = 1;

    public string? ImageUrl { get; set; }
}
