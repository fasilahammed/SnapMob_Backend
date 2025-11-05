//using SnapMob_Backend.Common;
//using SnapMob_Backend.DTOs.PaymentDTOs;
//using SnapMob_Backend.DTOs.OrderDTOs;
//using SnapMob_Backend.Enums;
//using SnapMob_Backend.Services.Interfaces;

//namespace SnapMob_Backend.Services.Implementation
//{
//    public class PaymentService : IPaymentService
//    {
//        private readonly IOrderService _orderService;

//        public PaymentService(IOrderService orderService)
//        {
//            _orderService = orderService;
//        }

//        public async Task<ApiResponse<object>> ProcessPaymentAsync(int userId, PaymentRequestDto dto)
//        {
//            // Only Cash on Delivery for now
//            if (dto.PaymentMethod.ToLower() != "cashondelivery")
//                return new ApiResponse<object>(400, "Only Cash on Delivery is supported currently.");

//            // Map to CreateOrderDto
//            var orderDto = new CreateOrderDto
//            {
//                FullName = dto.FullName,
//                PhoneNumber = dto.PhoneNumber,
//                Street = dto.Street,
//                City = dto.City,
//                State = dto.State,
//                ZipCode = dto.ZipCode,
//                Country = dto.Country
//            };

//            var result = await _orderService.CreateOrderAsync(userId, orderDto);

//            if (result.StatusCode != 200)
//                return new ApiResponse<object>(result.StatusCode, result.Message);

//            return new ApiResponse<object>(200, "Cash on Delivery order placed successfully", result.Data);
//        }
//    }
//}
