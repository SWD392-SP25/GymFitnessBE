using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class PaymentMehodRepository : IPaymentMethodRepository
    {
        private readonly GymbotDbContext _context;

        public PaymentMehodRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var paymentMethods = _context.PaymentMethods.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                paymentMethods = filterOn switch
                {
                    "methodname" => paymentMethods.Where(p => EF.Functions.Like(p.MethodName, $"%{filterQuery}%")),
                    "detail" => paymentMethods.Where(p => EF.Functions.Like(p.Detail, $"%{filterQuery}%")),
                    _ => paymentMethods
                };
            }

            // **Phân trang**
            paymentMethods = paymentMethods.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await paymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod?> GetByIdAsync(int methodId) =>
            await _context.PaymentMethods.FindAsync(methodId);

        public async Task AddAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task<PaymentMethod?> UpdateAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

        public async Task DeleteAsync(int methodId)
        {
            var entity = await _context.PaymentMethods.FindAsync(methodId);
            if (entity != null)
            {
                _context.PaymentMethods.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
