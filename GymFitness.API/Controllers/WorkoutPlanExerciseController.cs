using GymFitness.Application.Dtos;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanExerciseController : ControllerBase
    {
        private readonly WorkoutPlanExerciseService _service;

        public WorkoutPlanExerciseController(WorkoutPlanExerciseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exercises = await _service.GetAllWorkoutPlanExercisesAsync();
            return Ok(exercises);
        }

        [HttpGet("{planId}/{exerciseId}")]
        public async Task<IActionResult> GetById(int planId, int exerciseId)
        {
            var exercise = await _service.GetWorkoutPlanExerciseByIdAsync(planId, exerciseId);
            if (exercise == null)
                return NotFound();
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkoutPlanExerciseDto dto)
        {
            var exercise = new WorkoutPlanExercise
            {
                PlanId = dto.PlanId,
                ExerciseId = dto.ExerciseId,
                WeekNumber = dto.WeekNumber,
                DayOfWeek = dto.DayOfWeek,
                Sets = dto.Sets,
                Reps = dto.Reps,
                RestTimeSeconds = dto.RestTimeSeconds,
                Notes = dto.Notes
            };

            await _service.AddWorkoutPlanExerciseAsync(exercise);
            return CreatedAtAction(nameof(GetById), new { planId = exercise.PlanId, exerciseId = exercise.ExerciseId }, exercise);
        }

        [HttpPut("{planId}/{exerciseId}")]
        public async Task<IActionResult> Update(int planId, int exerciseId, [FromBody] WorkoutPlanExerciseDto dto)
        {
            var exercise = await _service.GetWorkoutPlanExerciseByIdAsync(planId, exerciseId);
            if (exercise == null)
                return NotFound();

            exercise.WeekNumber = dto.WeekNumber;
            exercise.DayOfWeek = dto.DayOfWeek;
            exercise.Sets = dto.Sets;
            exercise.Reps = dto.Reps;
            exercise.RestTimeSeconds = dto.RestTimeSeconds;
            exercise.Notes = dto.Notes;

            await _service.UpdateWorkoutPlanExerciseAsync(exercise);
            return NoContent();
        }

        [HttpDelete("{planId}/{exerciseId}")]
        public async Task<IActionResult> Delete(int planId, int exerciseId)
        {
            await _service.DeleteWorkoutPlanExerciseAsync(planId, exerciseId);
            return NoContent();
        }
    }
}
