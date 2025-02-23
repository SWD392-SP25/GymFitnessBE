using GymFitness.API.Dto;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseService _exerciseService;

        public ExerciseController(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exercises = await _exerciseService.GetAllExercisesAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);
            if (exercise == null)
                return NotFound();
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExerciseDto dto)
        {
            var exercise = new Exercise
            {
                Name = dto.Name,
                Description = dto.Description,
                MuscleGroupId = dto.MuscleGroupId,
                CategoryId = dto.CategoryId,
                DifficultyLevel = dto.DifficultyLevel,
                EquipmentNeeded = dto.EquipmentNeeded,
                VideoUrl = dto.VideoUrl,
                ImageUrl = dto.ImageUrl,
                Instructions = dto.Instructions,
                Precautions = dto.Precautions,
                CreatedAt = DateTime.UtcNow
            };

            await _exerciseService.AddExerciseAsync(exercise);
            return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExerciseDto dto)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);
            if (exercise == null)
                return NotFound();

            exercise.Name = dto.Name;
            exercise.Description = dto.Description;
            exercise.MuscleGroupId = dto.MuscleGroupId;
            exercise.CategoryId = dto.CategoryId;
            exercise.DifficultyLevel = dto.DifficultyLevel;
            exercise.EquipmentNeeded = dto.EquipmentNeeded;
            exercise.VideoUrl = dto.VideoUrl;
            exercise.ImageUrl = dto.ImageUrl;
            exercise.Instructions = dto.Instructions;
            exercise.Precautions = dto.Precautions;

            await _exerciseService.UpdateExerciseAsync(exercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exerciseService.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}
