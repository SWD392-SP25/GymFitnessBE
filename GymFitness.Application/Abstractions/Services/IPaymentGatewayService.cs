using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IPaymentGatewayService
    {
        Task<string> CreatePaymentUrl(decimal amount, string orderId, string returnUrl, string cancelUrl);
        Task<bool> VerifyPayment(string paymentId, string payerId);
    }

}
