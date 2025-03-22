using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IInvoicesRepository
    {
        Task<Invoice?> GetInvoiceById(int id);
        Task<List<Invoice>> GetInvoices(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10);
        Task AddInvoice(Invoice invoice);
       
        Task<List<Invoice?>> GetInvoiceByUser(string email);
    }
}
