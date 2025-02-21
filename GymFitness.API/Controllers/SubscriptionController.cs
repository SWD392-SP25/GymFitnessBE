using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanRepository _repository;

        public SubscriptionPlanController(ISubscriptionPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _repository.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionPlanDto dto)
        {
            var plan = new SubscriptionPlan
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DurationMonths = dto.DurationMonths,
                Features = dto.Features,
                MaxSessionsPerMonth = dto.MaxSessionsPerMonth,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(plan);
            return CreatedAtAction(nameof(GetById), new { id = plan.PlanId }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubscriptionPlanDto dto)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null)
                return NotFound();

            plan.Name = dto.Name;
            plan.Description = dto.Description;
            plan.Price = dto.Price;
            plan.DurationMonths = dto.DurationMonths;
            plan.Features = dto.Features;
            plan.MaxSessionsPerMonth = dto.MaxSessionsPerMonth;
            plan.IsActive = dto.IsActive;

            await _repository.UpdateAsync(plan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
