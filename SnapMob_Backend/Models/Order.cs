using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Enums;

namespace SnapMob_Backend.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CashOnDelivery;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public string BillingName { get; set; } = string.Empty;
        public string BillingPhone { get; set; } = string.Empty;
        public string BillingStreet { get; set; } = string.Empty;
        public string BillingCity { get; set; } = string.Empty;
        public string BillingState { get; set; } = string.Empty;
        public string BillingZip { get; set; } = string.Empty;
        public string BillingCountry { get; set; } = "India";

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
