using System;
using System.Collections.Generic;

namespace SnapMob_Backend.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
