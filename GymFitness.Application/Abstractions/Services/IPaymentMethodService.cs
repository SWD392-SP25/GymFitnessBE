using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodResponseDto>> GetAllPaymentMethodsAsync(string? filterOn, string? filterQuery, int pageNumber, int pageSize);
        Task<PaymentMethod?> GetPaymentMethodByIdAsync(int methodId);
        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task<PaymentMethodResponseDto> UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task DeletePaymentMethodAsync(int methodId);
    }
}
