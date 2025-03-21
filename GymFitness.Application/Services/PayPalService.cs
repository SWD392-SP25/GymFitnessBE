using GymFitness.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class PayPalService : IPaymentGatewayService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public PayPalService(IConfiguration configuration)
        {
            _clientId = configuration["PayPal:ClientId"];
            _clientSecret = configuration["PayPal:ClientSecret"];
        }

        private APIContext GetAPIContext()
        {
            var config = new Dictionary<string, string>
        {
            { "mode", "sandbox" } // "live" nếu deploy production
        };
            var accessToken = new OAuthTokenCredential(_clientId, _clientSecret, config).GetAccessToken();
            return new APIContext(accessToken) { Config = config };
        }

        public async Task<string> CreatePaymentUrl(decimal amount, string orderId, string returnUrl, string cancelUrl)
        {
            var apiContext = GetAPIContext();

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
            {
                new Transaction
                {
                    amount = new Amount { currency = "USD", total = amount.ToString("F2") },
                    description = $"Order {orderId}"
                }
            },
                redirect_urls = new RedirectUrls
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var createdPayment = await Task.Run(() => payment.Create(apiContext));

            // Lấy URL redirect đến PayPal
            var approvalUrl = createdPayment.links.FirstOrDefault(l => l.rel == "approval_url")?.href;
            return approvalUrl ?? throw new Exception("Không thể lấy URL thanh toán PayPal");
        }

        public async Task<bool> VerifyPayment(string paymentId, string payerId)
        {
            var apiContext = GetAPIContext();
            var payment = new Payment { id = paymentId };
            var executedPayment = await Task.Run(() => payment.Execute(apiContext, new PaymentExecution { payer_id = payerId }));

            return executedPayment.state.ToLower() == "approved";
        }
    }
}
