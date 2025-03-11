using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanService _planService;

        public SubscriptionPlanController(ISubscriptionPlanService planService)
        {
            _planService = planService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()   
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
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

            await _planService.AddPlanAsync(plan);
            return CreatedAtAction(nameof(GetById), new { id = plan.PlanId }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubscriptionPlanDto dto)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null)
                return NotFound();

            plan.Name = dto.Name;
            plan.Description = dto.Description;
            plan.Price = dto.Price;
            plan.DurationMonths = dto.DurationMonths;
            plan.Features = dto.Features;
            plan.MaxSessionsPerMonth = dto.MaxSessionsPerMonth;
            plan.IsActive = dto.IsActive;

            await _planService.UpdatePlanAsync(plan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _planService.DeletePlanAsync(id);
            return NoContent();
        }
    }
}
