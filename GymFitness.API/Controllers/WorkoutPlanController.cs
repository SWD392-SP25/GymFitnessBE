using Azure;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Dtos;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly IStaffService _staffService;
        private readonly IUserSubscriptionService _userSubscriptionService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService, IStaffService staffService, IUserSubscriptionService userSubscriptionService)
        {
            _workoutPlanService = workoutPlanService;
            _staffService = staffService;
            _userSubscriptionService = userSubscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction([FromQuery] string? filterOn,
                                                  [FromQuery] string? filterQuery,
                                                  [FromQuery] string sortBy = "difficulty",
                                                  [FromQuery] bool? isAscending = true,
                                                  [FromQuery] int pageNumber = 1,
                                                  [FromQuery] int pageSize = 10)
        {
            var workoutPlans = await _workoutPlanService.GetWorkoutPlans(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(workoutPlans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workoutPlan = await _workoutPlanService.GetWorkoutPlanById(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            return Ok(workoutPlan);
        }
        [HttpGet("subscription/{id}")]
        public async Task<IActionResult> GetBySubscriptionPlanId(int id)
        {
            var workoutPlans = await _workoutPlanService.GetWorkoutPlansBySubscriptionPlanId(id);
            if (workoutPlans == null)
            {
                return NotFound();
            }
            return Ok(workoutPlans);
        }

        [HttpPost("create/{id}")]
        public async Task<IActionResult> Create(int id, [FromBody] WorkoutPlanDto workoutPlan)
        {
            var staff = await _staffService.GetByEmailAsync(workoutPlan.CreatedBy);
            if (staff == null)
            {
                return BadRequest("Staff not found");
            }
            var userSubscription = await _userSubscriptionService.GetUserSubscriptionById(id);

            if (userSubscription == null)
            {
                return BadRequest("Subscription Plan not found");
            }
            var workoutPlanEntity = new WorkoutPlan
            {
                Name = workoutPlan.Name,
                Description = workoutPlan.Description,
                DifficultyLevel = workoutPlan.DifficultyLevel,
                DurationWeeks = workoutPlan.DurationWeeks,
                TargetAudience = workoutPlan.TargetAudience,
                Goals = workoutPlan.Goals,
                Prerequisites = workoutPlan.Prerequisites,

                CreatedBy = staff.StaffId,
                SubscriptionPlanId = id
            };
            await _workoutPlanService.AddWorkoutPlan(workoutPlanEntity);
            return Ok(workoutPlanEntity);
        }

        [HttpPatch("id")]
        [Consumes("application/json")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<WorkoutPlanDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var workoutPlan = await _workoutPlanService.GetWorkoutPlanById(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            var workoutPlanDto = new WorkoutPlanDto
            {
                Name = workoutPlan.Name,
                Description = workoutPlan.Description,
                DifficultyLevel = workoutPlan.DifficultyLevel,
                DurationWeeks = workoutPlan.DurationWeeks,
                CreatedBy = workoutPlan.CreatedByNavigation?.Email,
                TargetAudience = workoutPlan.TargetAudience,
                Goals = workoutPlan.Goals,
                Prerequisites = workoutPlan.Prerequisites
            };

            patchDoc.ApplyTo(workoutPlanDto, error =>
            {
                ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff = workoutPlanDto.CreatedBy != null ? await _staffService.GetByEmailAsync(workoutPlanDto.CreatedBy) : null;

            if(staff == null)
            {
                return BadRequest("Staff not found");
            }

            workoutPlan.Name = workoutPlanDto.Name;
            workoutPlan.Description = workoutPlanDto.Description;
            workoutPlan.DifficultyLevel = workoutPlanDto.DifficultyLevel;
            workoutPlan.DurationWeeks = workoutPlanDto.DurationWeeks;
            workoutPlan.TargetAudience = workoutPlanDto.TargetAudience;
            workoutPlan.Goals = workoutPlanDto.Goals;
            workoutPlan.Prerequisites = workoutPlanDto.Prerequisites;
            workoutPlan.CreatedBy = staff.StaffId;

            // Chặn cập nhật CreatedAt
            var updatedProperties = patchDoc.Operations
                .Select(o => o.path.TrimStart('/'))
                .Where(p => p != "CreatedAt")
                .ToList();

            await _workoutPlanService.UpdateWorkoutPlan(workoutPlan, updatedProperties);
            return Ok(workoutPlan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workoutPlan = await _workoutPlanService.GetWorkoutPlanById(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            await _workoutPlanService.DeleteWorkoutPlan(id);
            return Ok();
        }
    }
}
