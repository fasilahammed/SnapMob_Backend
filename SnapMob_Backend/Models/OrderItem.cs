using Microsoft.EntityFrameworkCore;

namespace SnapMob_Backend.Models
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Name { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
