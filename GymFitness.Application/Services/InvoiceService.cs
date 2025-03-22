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
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoicesRepository _invoiceRepository;

        public InvoiceService(IInvoicesRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }


        public async Task AddInvoice(Invoice invoice)
        {
            await _invoiceRepository.AddInvoice(invoice);
        }

        public async Task<Invoice?> GetInvoiceById(int id)
        {
            return await _invoiceRepository.GetInvoiceById(id);
        }

        public async Task<List<InvoiceResponseDto?>> GetInvoiceByUser(string email)
        {
            var invoices = await  _invoiceRepository.GetInvoiceByUser(email);
            return invoices.Select(a => new InvoiceResponseDto
            {
                InvoiceId = a.InvoiceId,
                UserId = a.User.UserId,
                UserName = a.User.Email ?? "Not found",
                SubscriptionId = a.SubscriptionId,
                Amount = a.Amount,
                Status = a.Status,
                DueDate = a.DueDate,
                PaidDate = a.PaidDate,
            
                PaymentMethod = a.PaymentMethod.MethodName,
                CreatedAt = a.CreatedAt,
                
                
               
            }).ToList();

        //            public int InvoiceId { get; set; }

        //public Guid UserId { get; set; }
        //public string? UserName { get; set; }


        ////public int? SubscriptionId { get; set; }

        //public decimal? Amount { get; set; }

        //public string? Status { get; set; }

        //public DateOnly? DueDate { get; set; }

        //public DateOnly? PaidDate { get; set; }

        ////public int? PaymentMethodId { get; set; }
        //public string? PaymentMethod { get; set; }

        //public DateTime? CreatedAt { get; set; }

        }

        public async Task<List<InvoiceResponseDto>> GetInvoices(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var invoices = await _invoiceRepository.GetInvoices(filterOn, filterQuery, pageNumber, pageSize);
            return invoices.Select(a => new InvoiceResponseDto
            {
                InvoiceId = a.InvoiceId,
                UserId = a.User.UserId,
                UserName = a.User.Email ?? "Not found",
                SubscriptionId = a.SubscriptionId,
                Amount = a.Amount,
                Status = a.Status,
                DueDate = a.DueDate,
                PaidDate = a.PaidDate,
                PaymentMethod = a.PaymentMethod.MethodName ?? "Unknow",
                CreatedAt = a.CreatedAt,
            }).ToList();
        }
    }
}
