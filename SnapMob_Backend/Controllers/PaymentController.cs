//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SnapMob_Backend.DTOs.PaymentDTOs;
//using SnapMob_Backend.Services.Interfaces;
//using System.Security.Claims;

//namespace SnapMob_Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class PaymentController : ControllerBase
//    {
//        private readonly IPaymentService _paymentService;

//        public PaymentController(IPaymentService paymentService)
//        {
//            _paymentService = paymentService;
//        }

//        // ✅ Handle Cash on Delivery (and future Razorpay)
//        [HttpPost("create")]
//        [Authorize(Policy = "Customer")]
//        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto dto)
//        {
//            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (string.IsNullOrEmpty(userIdClaim))
//                return Unauthorized("User ID not found in token.");

//            int userId = int.Parse(userIdClaim);

//            var response = await _paymentService.ProcessPaymentAsync(userId, dto);
//            return StatusCode(response.StatusCode, response);
//        }
//    }
//}
