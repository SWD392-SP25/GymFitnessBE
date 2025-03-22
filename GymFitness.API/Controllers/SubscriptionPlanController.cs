using Azure;
using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionPlanController : Controller
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;

        public SubscriptionPlanController(ISubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionPlan([FromQuery] string? filterOn,
                                                            [FromQuery] string? filterQuery,
                                                            [FromQuery] string? sortBy,
                                                            [FromQuery] bool? isAscending,
                                                            [FromQuery] int pageNumber = 1,
                                                            [FromQuery] int pageSize = 10)
        {
            var subscriptionPlans = await _subscriptionPlanService.GetSubscriptionPlan(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(subscriptionPlans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscriptionPlanById(int id)
        {
            var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlanById(id);
            if (subscriptionPlan == null)
            {
                return NotFound();
            }
            return Ok(subscriptionPlan);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriptionPlan([FromBody] SubscriptionPlanDto input)
        {
            var subscriptionPlan = new SubscriptionPlan{
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                DurationMonths = input.DurationMonths,
                Features = input.Features,               
                IsActive = input.IsActive,
                MaxSessionsPerMonth = input.MaxSessionsPerMonth
            };

        await _subscriptionPlanService.CreateSubscriptionPlan(subscriptionPlan);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriptionPlan(int id)
        {
            await _subscriptionPlanService.DeleteSubscriptionPlan(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPlanDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlanById(id);
            if (subscriptionPlan == null)
            {
                return NotFound();
            }
            var subscriptionPlanDto = new SubscriptionPlanDto
            {
                
                Name = subscriptionPlan.Name,
                Description = subscriptionPlan.Description,
                Price = subscriptionPlan.Price,
                DurationMonths = subscriptionPlan.DurationMonths,
                Features = subscriptionPlan.Features,
                MaxSessionsPerMonth = subscriptionPlan.MaxSessionsPerMonth,
                IsActive = subscriptionPlan.IsActive
            };

            patchDoc.ApplyTo(subscriptionPlanDto, error =>
            {
                ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            subscriptionPlan.Name = subscriptionPlanDto.Name;
            subscriptionPlan.Description = subscriptionPlanDto.Description;
            subscriptionPlan.Price = subscriptionPlanDto.Price;
            subscriptionPlan.DurationMonths = subscriptionPlanDto.DurationMonths;
            subscriptionPlan.Features = subscriptionPlanDto.Features;
            subscriptionPlan.MaxSessionsPerMonth = subscriptionPlanDto.MaxSessionsPerMonth;
            subscriptionPlan.IsActive = subscriptionPlanDto.IsActive;

            // Chặn cập nhật CreatedAt
            var updatedProperties = patchDoc.Operations
                .Select(o => o.path.TrimStart('/'))
                .Where(p => p != "CreatedAt")
                .ToList();

            await _subscriptionPlanService.UpdateSubscriptionPlan(subscriptionPlan, updatedProperties);
            return Ok(subscriptionPlan);
        }
    }
}
