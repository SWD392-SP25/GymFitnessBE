using GymFitness.API.RequestDto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IConfiguration _configuration;

        public PaymentController(ISubscriptionPlanService subscriptionPlanService,
                                 IUserSubscriptionService userSubscriptionService,
                                 IPaymentGatewayService paymentGatewayService,
                                 IConfiguration configuration)
        {
            _subscriptionPlanService = subscriptionPlanService;
            _userSubscriptionService = userSubscriptionService;
            _paymentGatewayService = paymentGatewayService;
            _configuration = configuration;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> CreateSubscriptionAndPay([FromBody] UserSubscriptionRequestDto request)
        {
            if (request == null || request.SubscriptionPlanId <= 0)
            {
                return BadRequest("Invalid subscription plan ID.");
            }

            // 🔹 1. Lấy thông tin gói đăng ký
            var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlanById(request.SubscriptionPlanId);
            if (subscriptionPlan == null)
            {
                return NotFound("Subscription plan not found.");
            }

            // 🔹 2. Lấy UserId từ Bearer Token (chuyển đổi thành Guid)
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Invalid or missing User ID.");
            }

            // 🔹 3. Tạo bản ghi User Subscription (trạng thái Pending)
            var userSubscription = new UserSubscription
            {
                UserId = userId,
                SubscriptionPlanId = request.SubscriptionPlanId,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(subscriptionPlan.DurationMonths)),
                Status = "Pending",
                PaymentFrequency = request.PaymentFrequency,
                AutoRenew = request.AutoRenew,
                CreatedAt = DateTime.UtcNow
            };

            await _userSubscriptionService.AddUserSubcription(userSubscription);

            // 🔹 4. Tạo URL thanh toán với PayPal
            var returnUrl = _configuration["PayPal:ReturnUrl"];
            var cancelUrl = _configuration["PayPal:CancelUrl"];
            var paymentUrl = await _paymentGatewayService.CreatePaymentUrl(
                subscriptionPlan.Price,
                userSubscription.SubscriptionId.ToString(),
                returnUrl,
                cancelUrl
            );

            return Ok(new { PaymentUrl = paymentUrl, SubscriptionId = userSubscription.SubscriptionId });
        }


        //[HttpPost("execute-payment")]
        //public async Task<IActionResult> ExecutePayment([FromQuery] int subscriptionId, [FromQuery] string payerId)
        //{
        //    // 🔹 1. Xác nhận thanh toán với PayPal
        //    var success = await _paymentGatewayService.VerifyPayment(subscriptionId.ToString(), payerId);
        //    if (!success)
        //    {
        //        return BadRequest("Payment execution failed.");
        //    }

        //    // 🔹 2. Tìm User Subscription có trạng thái Pending dựa trên SubscriptionId
        //    var userSubscription = await _userSubscriptionService.GetPendingSubscriptionBySubscriptionId(subscriptionId);
        //    if (userSubscription == null)
        //    {
        //        return NotFound("Pending subscription not found.");
        //    }

        //    // 🔹 3. Cập nhật trạng thái đăng ký thành Active
        //    userSubscription.Status = "Active";
        //    await _userSubscriptionService.UpdateUserSubscription(userSubscription, new List<string> { "Status" });

        //    return Ok("Subscription activated successfully.");
        //}

        [HttpPost("execute-payment")]
        public async Task<IActionResult> ExecutePayment([FromQuery] string paymentId, [FromQuery] string payerId, [FromQuery] int subscriptionId)
        {
            // 🔹 1. Xác nhận thanh toán với PayPal
            var success = await _paymentGatewayService.VerifyPayment(paymentId, payerId);
            if (!success)
            {
                return BadRequest("Payment execution failed.");
            }

            // 🔹 2. Tìm User Subscription có trạng thái Pending dựa trên SubscriptionId
            var userSubscription = await _userSubscriptionService.GetPendingSubscriptionBySubscriptionId(subscriptionId);
            if (userSubscription == null)
            {
                return NotFound("Pending subscription not found.");
            }

            // 🔹 4. Cập nhật trạng thái đăng ký thành Active
            userSubscription.Status = "Active";
            await _userSubscriptionService.UpdateUserSubscription(userSubscription, new List<string> { "Status" });

            return Ok("Subscription activated successfully.");
        }

    }
}
