using System.ComponentModel.DataAnnotations.Schema;

namespace SnapMob_Backend.Models
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtAddTime { get; set; }

        // 🧭 Navigation
        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }
}
