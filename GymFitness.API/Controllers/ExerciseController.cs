using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseService _exerciseService;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly List<string> _allowedImageExtensions = new() { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly List<string> _allowedVideoExtensions = new() { ".mp4", ".mov", ".avi", ".mkv" };

        public ExerciseController(ExerciseService exerciseService, IFirebaseStorageService firebaseStorageService)
        {
            _exerciseService = exerciseService;
            _firebaseStorageService = firebaseStorageService;
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
        public async Task<IActionResult> Create([FromForm] ExerciseCreateDto dto)
        {
            string imageUrl = null, videoUrl = null;

            // Upload ảnh nếu có
            if (dto.ImageFile != null)
            {
                if (!_allowedImageExtensions.Contains(Path.GetExtension(dto.ImageFile.FileName).ToLower()))
                    return BadRequest("Chỉ hỗ trợ định dạng ảnh: .jpg, .jpeg, .png, .gif");

                imageUrl = await _firebaseStorageService.UploadFileAsync(dto.ImageFile, "exercise_images");
            }

            // Upload video nếu có
            if (dto.VideoFile != null)
            {
                if (!_allowedVideoExtensions.Contains(Path.GetExtension(dto.VideoFile.FileName).ToLower()))
                    return BadRequest("Chỉ hỗ trợ định dạng video: .mp4, .mov, .avi, .mkv");

                videoUrl = await _firebaseStorageService.UploadFileAsync(dto.VideoFile, "exercise_videos");
            }

            var exercise = new Exercise
            {
                Name = dto.Name,
                Description = dto.Description,
                MuscleGroupId = dto.MuscleGroupId,
                CategoryId = dto.CategoryId,
                DifficultyLevel = dto.DifficultyLevel,
                EquipmentNeeded = dto.EquipmentNeeded,
                VideoUrl = videoUrl,
                ImageUrl = imageUrl,
                Instructions = dto.Instructions,
                Precautions = dto.Precautions,
                CreatedAt = DateTime.UtcNow
            };

            await _exerciseService.AddExerciseAsync(exercise);
            return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ExerciseUpdateDto dto)
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
            exercise.Instructions = dto.Instructions;
            exercise.Precautions = dto.Precautions;

            // Cập nhật ảnh nếu có
            if (dto.ImageFile != null)
            {
                if (!_allowedImageExtensions.Contains(Path.GetExtension(dto.ImageFile.FileName).ToLower()))
                    return BadRequest("Chỉ hỗ trợ định dạng ảnh: .jpg, .jpeg, .png, .gif");

                exercise.ImageUrl = await _firebaseStorageService.UploadFileAsync(dto.ImageFile, "exercise_images");
            }

            // Cập nhật video nếu có
            if (dto.VideoFile != null)
            {
                if (!_allowedVideoExtensions.Contains(Path.GetExtension(dto.VideoFile.FileName).ToLower()))
                    return BadRequest("Chỉ hỗ trợ định dạng video: .mp4, .mov, .avi, .mkv");

                exercise.VideoUrl = await _firebaseStorageService.UploadFileAsync(dto.VideoFile, "exercise_videos");
            }

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
