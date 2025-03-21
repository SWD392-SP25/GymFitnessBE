using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IEnumerable<PaymentMethodResponseDto>> GetAllPaymentMethodsAsync(string? filterOn, string? filterQuery, int pageNumber, int pageSize)
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);
            return paymentMethods.Select(p => new PaymentMethodResponseDto
            {
                MethodId = p.MethodId,
                MethodName = p.MethodName,
                Detail = p.Detail
            }).ToList();
        }

        public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(int methodId) =>
            await _paymentMethodRepository.GetByIdAsync(methodId);

        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod) =>
            await _paymentMethodRepository.AddAsync(paymentMethod);

        public async Task<PaymentMethodResponseDto> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            var updatedPaymentMethod = await _paymentMethodRepository.UpdateAsync(paymentMethod);
            return new PaymentMethodResponseDto
            {
                MethodId = updatedPaymentMethod.MethodId,
                MethodName = updatedPaymentMethod.MethodName,
                Detail = updatedPaymentMethod.Detail
            };
        }

        public async Task DeletePaymentMethodAsync(int methodId) =>
            await _paymentMethodRepository.DeleteAsync(methodId);
    }
}
