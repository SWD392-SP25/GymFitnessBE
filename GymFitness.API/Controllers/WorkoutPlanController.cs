using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Dtos;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _service;

        public WorkoutPlanController(IWorkoutPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllWorkoutPlansAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _service.GetWorkoutPlanByIdAsync(id);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkoutPlanDto dto)
        {
            var plan = new WorkoutPlan
            {
                Name = dto.Name,
                Description = dto.Description,
                DifficultyLevel = dto.DifficultyLevel,
                DurationWeeks = dto.DurationWeeks,
                CreatedBy = dto.CreatedBy,
                TargetAudience = dto.TargetAudience,
                Goals = dto.Goals,
                Prerequisites = dto.Prerequisites,
                CreatedAt = DateTime.UtcNow
            };

            await _service.AddWorkoutPlanAsync(plan);
            return CreatedAtAction(nameof(GetById), new { id = plan.PlanId }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkoutPlanDto dto)
        {
            var plan = await _service.GetWorkoutPlanByIdAsync(id);
            if (plan == null)
                return NotFound();

            plan.Name = dto.Name;
            plan.Description = dto.Description;
            plan.DifficultyLevel = dto.DifficultyLevel;
            plan.DurationWeeks = dto.DurationWeeks;
            plan.CreatedBy = dto.CreatedBy;
            plan.TargetAudience = dto.TargetAudience;
            plan.Goals = dto.Goals;
            plan.Prerequisites = dto.Prerequisites;

            await _service.UpdateWorkoutPlanAsync(plan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteWorkoutPlanAsync(id);
            return NoContent();
        }
    }
}
