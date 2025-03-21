using GymFitness.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/paypal")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IConfiguration _configuration;

        public PayPalController(IPaymentGatewayService paymentGatewayService, IConfiguration configuration)
        {
            _paymentGatewayService = paymentGatewayService;
            _configuration = configuration;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] decimal amount)
        {
            var orderId = Guid.NewGuid().ToString();
            var returnUrl = _configuration["PayPal:ReturnUrl"];
            var cancelUrl = _configuration["PayPal:CancelUrl"];

            var paymentUrl = await _paymentGatewayService.CreatePaymentUrl(amount, orderId, returnUrl, cancelUrl);
            return Ok(new { PaymentUrl = paymentUrl });
        }

        [HttpGet("verify-payment")]
        public async Task<IActionResult> VerifyPayment(string paymentId, string payerId)
        {
            var result = await _paymentGatewayService.VerifyPayment(paymentId, payerId);
            return result ? Ok("Thanh toán thành công") : BadRequest("Thanh toán thất bại");
        }


    }
}
