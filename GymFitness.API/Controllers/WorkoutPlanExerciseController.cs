using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Dtos;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutPlanExerciseController : ControllerBase
    {
        private readonly IWorkoutPlanExerciseService _workoutPlanExerciseService;
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutPlanExerciseController(IWorkoutPlanExerciseService workoutPlanExerciseService, IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanExerciseService = workoutPlanExerciseService;
            _workoutPlanService = workoutPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction([FromQuery] string? filterOn,
                                                  [FromQuery] string? filterQuery,
                                                  [FromQuery] string sortBy = "difficulty",
                                                  [FromQuery] bool? isAscending = true,
                                                  [FromQuery] int pageNumber = 1,
                                                  [FromQuery] int pageSize = 10)
        {
            var workoutPlanExercises = await _workoutPlanExerciseService.GetWorkoutPlanExercisesAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(workoutPlanExercises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workoutPlanExercise = await _workoutPlanExerciseService.GetWorkoutPlanExerciseByIdAsync(id);
            if (workoutPlanExercise == null)
            {
                return NotFound();
            }
            return Ok(workoutPlanExercise);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(int id, [FromBody] WorkoutPlanExerciseDto input)
        {
            var isExist = _workoutPlanService.GetWorkoutPlanById(id);
            if (isExist == null)
            {
                return BadRequest("Workout Plan not found");
            }
            var workoutPlanExercise = new WorkoutPlanExercise
            {
               
                ExerciseId = input.ExerciseId,
                WeekNumber = input.WeekNumber,
                DayOfWeek = input.DayOfWeek,
                Sets = input.Sets,
                Reps = input.Reps,
                RestTimeSeconds = input.RestTimeSeconds,
                Notes = input.Notes
            };
            await _workoutPlanExerciseService.AddAsync(workoutPlanExercise);
            return Ok();
        }

        [HttpPatch("{id}")]
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<WorkoutPlanExerciseDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var workoutPlanExercise = await _workoutPlanExerciseService.GetWorkoutPlanExerciseByIdAsync(id);
            if (workoutPlanExercise == null)
            {
                return NotFound();
            }
            var workoutPlanExerciseDto = new WorkoutPlanExerciseDto
            {
             
                ExerciseId = workoutPlanExercise.Exercise.ExerciseId,
                WeekNumber = workoutPlanExercise.WeekNumber,
                DayOfWeek = workoutPlanExercise.DayOfWeek,
                Sets = workoutPlanExercise.Sets,
                Reps = workoutPlanExercise.Reps,
                RestTimeSeconds = workoutPlanExercise.RestTimeSeconds,
                Notes = workoutPlanExercise.Notes
            };
            patchDoc.ApplyTo(workoutPlanExerciseDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
     
            workoutPlanExercise.Exercise.ExerciseId = workoutPlanExerciseDto.ExerciseId;
            workoutPlanExercise.WeekNumber = workoutPlanExerciseDto.WeekNumber;
            workoutPlanExercise.DayOfWeek = workoutPlanExerciseDto.DayOfWeek;
            workoutPlanExercise.Sets = workoutPlanExerciseDto.Sets;
            workoutPlanExercise.Reps = workoutPlanExerciseDto.Reps;
            workoutPlanExercise.RestTimeSeconds = workoutPlanExerciseDto.RestTimeSeconds;
            workoutPlanExercise.Notes = workoutPlanExerciseDto.Notes;

            // Chặn cập nhật CreatedAt
            var updatedProperties = patchDoc.Operations
                .Select(o => o.path.TrimStart('/'))
                .Where(p => p != "CreatedAt")
                .ToList();
            await _workoutPlanExerciseService.UpdateAsync(workoutPlanExercise, updatedProperties);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _workoutPlanExerciseService.DeleteAsync(id);
            return Ok();
        }
    }
}
