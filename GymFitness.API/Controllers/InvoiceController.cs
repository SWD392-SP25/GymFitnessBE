using GymFitness.API.RequestDto;
using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        //Task<Invoice?> GetInvoiceById(int id);
        //Task<List<InvoiceResponseDto>> GetInvoices(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10);
        //Task AddInvoice(Invoice invoice);

        //Task<List<InvoiceResponseDto?>> GetInvoiceByUser(string email);
        public InvoiceController(IInvoiceService invoiceService, IUserService userService)
        {
            _invoiceService = invoiceService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAction([FromQuery] string? filterOn,
                                                  [FromQuery] string? filterQuery,
                                                  [FromQuery] int pageNumber = 1,
                                                  [FromQuery] int pageSize = 10)
        {
            var invoices = await _invoiceService.GetInvoices(filterOn, filterQuery, pageNumber, pageSize);
            return Ok(invoices);
        }
        [HttpGet("/invoice/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }
        [HttpGet("/invoice/user/{email}")]
        public async Task<IActionResult> GetByUser(string email)
        {
            var invoices = await _invoiceService.GetInvoiceByUser(email);
            if (invoices == null)
            {
                return NotFound();
            }
            return Ok(invoices);
        }

        //public string? UserEmail { get; set; }

        //public int? SubscriptionId { get; set; }

        //public decimal? Amount { get; set; }

        //public string? Status { get; set; }

        //public DateOnly? DueDate { get; set; }

        //public DateOnly? PaidDate { get; set; }

        //public int? PaymentMethodId { get; set; }

        //public DateTime? CreatedAt { get; set; }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceDto invoice)
        {
            var user = await _userService.GetUserByEmail(invoice.UserEmail);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var invoiceEntity = new Invoice
            {
                UserId = user.UserId,
                SubscriptionId = invoice.SubscriptionId,
                Amount = invoice.Amount,
                Status = invoice.Status,
                DueDate = invoice.DueDate,
                PaidDate = invoice.PaidDate,
                PaymentMethodId = invoice.PaymentMethodId,
                CreatedAt = invoice.CreatedAt
            };
            await _invoiceService.AddInvoice(invoiceEntity);
            return Ok();
        }

    }
}
