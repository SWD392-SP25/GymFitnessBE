using GymFitness.API.RequestDto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _service;

        public PaymentMethodController(IPaymentMethodService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var paymentMethods = await _service.GetAllPaymentMethodsAsync(filterOn, filterQuery, pageNumber, pageSize);
            return Ok(paymentMethods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paymentMethod = await _service.GetPaymentMethodByIdAsync(id);
            if (paymentMethod == null)
                return NotFound();
            return Ok(paymentMethod);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentMethodRequestDto dto)
        {
            var paymentMethod = new PaymentMethod
            {
                MethodName = dto.MethodName,
                Detail = dto.Detail
            };

            await _service.AddPaymentMethodAsync(paymentMethod);
            return CreatedAtAction(nameof(GetById), new { id = paymentMethod.MethodId }, paymentMethod);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentMethodRequestDto dto)
        {
            var paymentMethod = await _service.GetPaymentMethodByIdAsync(id);
            if (paymentMethod == null)
                return NotFound();

            paymentMethod.MethodName = dto.MethodName;
            paymentMethod.Detail = dto.Detail;

            await _service.UpdatePaymentMethodAsync(paymentMethod);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePaymentMethodAsync(id);
            return NoContent();
        }
    }
}
