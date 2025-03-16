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
    public class InvoiceRepository : IInvoicesRepository
    {
        private readonly GymbotDbContext _context;

        public InvoiceRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public async Task AddInvoice(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<Invoice?> GetInvoiceById(int id)
        {
            return await _context.Invoices.Include(x => x.PaymentMethod)
                                          .Include(x => x.User)
                                          .FirstOrDefaultAsync(x => x.InvoiceId.Equals(id));
        }

        public async Task<List<Invoice?>> GetInvoiceByUser(string email)
        {
            return await _context.Invoices.Include(x => x.PaymentMethod)
                                          .Include(x => x.User)
                                                    .Where(x => x.User.Email.ToLower() == email.ToLower()).ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoices(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var invoices = _context.Invoices.Include(x => x.PaymentMethod)
                                            .Include(x => x.User)
                                            .AsQueryable();
            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                invoices = filterOn switch
                {
                    "email" => invoices.Where(x => x.User.Email != null && x.User.Email.ToLower().Contains(filterQuery)),
                    "status" => invoices.Where(x => x.Status != null && x.Status.ToLower().Contains(filterQuery)),
                    "location" => invoices.Where(x => x.PaymentMethod != null && x.PaymentMethod.MethodName.ToLower().Contains(filterQuery)),
                    _ => invoices
                };

          
            }
            // **Phân trang**
            invoices = invoices.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize);
            return await invoices.ToListAsync();
        }

        public async Task UpdateInvoice(Invoice invoice, List<string> updatedProperties)
        {
            var entry = _context.Entry(invoice);
            foreach (var property in updatedProperties)
            {
                if (_context.Entry(invoice).Properties.Any(p => p.Metadata.Name == property))
                {
                    entry.Property(property).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
