using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> GetAllAsync(string? filterOn, string? filterQuery, int pageNumber, int pageSize);
        Task<PaymentMethod?> GetByIdAsync(int methodId);
        Task AddAsync(PaymentMethod paymentMethod);
        Task<PaymentMethod?> UpdateAsync(PaymentMethod paymentMethod);
        Task DeleteAsync(int methodId);

    }
}
