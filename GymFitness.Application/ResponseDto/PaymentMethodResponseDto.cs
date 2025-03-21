using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class PaymentMethodResponseDto
    {
        public int MethodId { get; set; }
        public string? MethodName { get; set; }
        public string? Detail { get; set; }
    }
}
