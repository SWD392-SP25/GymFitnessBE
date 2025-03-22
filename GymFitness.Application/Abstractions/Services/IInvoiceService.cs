using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IInvoiceService
    {
        Task<Invoice?> GetInvoiceById(int id);
        Task<List<InvoiceResponseDto>> GetInvoices(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10);
        Task AddInvoice(Invoice invoice);

        Task<List<InvoiceResponseDto?>> GetInvoiceByUser(string email);
    }
}
