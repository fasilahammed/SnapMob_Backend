using SnapMob_Backend.Enums;

namespace SnapMob_Backend.DTOs.OrderDTOs
{
    public class UpdateOrderStatusDto
    {
        public OrderStatus NewStatus { get; set; }
    }
}
