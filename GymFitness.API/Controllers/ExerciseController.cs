using Azure;
using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {

        private readonly IExerciseService _exerciseService;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly List<string> _allowedImageExtensions = new() { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly List<string> _allowedVideoExtensions = new() { ".mp4", ".mov", ".avi", ".mkv" };

        public ExerciseController(IExerciseService exerciseService, IFirebaseStorageService firebaseStorageService)
        {
            _exerciseService = exerciseService;
            _firebaseStorageService = firebaseStorageService;
        }


        [HttpGet]
 
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sortBy,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var exercises = await _exerciseService.GetAllExercisesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
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


        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromForm] ExerciseUpdateDto dto)
        //{
        //    var exercise = await _exerciseService.GetExerciseByIdAsync(id);
        //    if (exercise == null)
        //        return NotFound();

        //    exercise.Name = dto.Name;
        //    exercise.Description = dto.Description;
        //    exercise.MuscleGroupId = dto.MuscleGroupId;
        //    exercise.CategoryId = dto.CategoryId;
        //    exercise.DifficultyLevel = dto.DifficultyLevel;
        //    exercise.EquipmentNeeded = dto.EquipmentNeeded;
        //    exercise.Instructions = dto.Instructions;
        //    exercise.Precautions = dto.Precautions;

        //    // Cập nhật ảnh nếu có
        //    if (dto.ImageFile != null)
        //    {
        //        if (!_allowedImageExtensions.Contains(Path.GetExtension(dto.ImageFile.FileName).ToLower()))
        //            return BadRequest("Chỉ hỗ trợ định dạng ảnh: .jpg, .jpeg, .png, .gif");

        //        exercise.ImageUrl = await _firebaseStorageService.UploadFileAsync(dto.ImageFile, "exercise_images");
        //    }

        //    // Cập nhật video nếu có
        //    if (dto.VideoFile != null)
        //    {
        //        if (!_allowedVideoExtensions.Contains(Path.GetExtension(dto.VideoFile.FileName).ToLower()))
        //            return BadRequest("Chỉ hỗ trợ định dạng video: .mp4, .mov, .avi, .mkv");

        //        exercise.VideoUrl = await _firebaseStorageService.UploadFileAsync(dto.VideoFile, "exercise_videos");
        //    }

        //    await _exerciseService.UpdateExerciseAsync(exercise);
        //    return NoContent();
        //}

        [HttpPatch("{id}")]
        [Consumes("application/json-patch+json")]
   
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ExercisePatchDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null");
            }

            var exercise = await _exerciseService.GetExerciseByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            var exercisePatch = new ExercisePatchDto
            {
                Name = exercise.Name,
                Description = exercise.Description,
                MuscleGroupId = exercise.MuscleGroupId,
                CategoryId = exercise.CategoryId,
                DifficultyLevel = exercise.DifficultyLevel,
                EquipmentNeeded = exercise.EquipmentNeeded,
                Instructions = exercise.Instructions,
                Precautions = exercise.Precautions
            };

            patchDoc.ApplyTo(exercisePatch, error =>
            {
                ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            exercise.Name = exercisePatch.Name;
            exercise.Description = exercisePatch.Description;
            exercise.MuscleGroupId = exercisePatch.MuscleGroupId;
            exercise.CategoryId = exercisePatch.CategoryId;
            exercise.DifficultyLevel = exercisePatch.DifficultyLevel;
            exercise.EquipmentNeeded = exercisePatch.EquipmentNeeded;
            exercise.Instructions = exercisePatch.Instructions;
            exercise.Precautions = exercisePatch.Precautions;

            // Chặn cập nhật CreatedAt
            var updatedProperties = patchDoc.Operations
                .Select(o => o.path.TrimStart('/'))
                .Where(p => p != "CreatedAt")
                .ToList();

            await _exerciseService.UpdateExerciseAsync(exercise, updatedProperties);
            return Ok();
        }

        [HttpPatch("{id}/upload")]
        [Consumes("multipart/form-data")]
 
        public async Task<IActionResult> PatchUpload(int id, [FromForm] ExerciseFileDto dto)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);
            if (exercise == null)
                return NotFound();

            var updatedProperties = new List<string>();

            // Xử lý ảnh
            if (dto.ImageFile != null)
            {
                if (!_allowedImageExtensions.Contains(Path.GetExtension(dto.ImageFile.FileName).ToLower()))
                    return BadRequest("Chỉ hỗ trợ định dạng ảnh: .jpg, .jpeg, .png, .gif");

                exercise.ImageUrl = await _firebaseStorageService.UploadFileAsync(dto.ImageFile, "exercise_images");
                updatedProperties.Add(nameof(exercise.ImageUrl));
            }
                
            // Xử lý video
            if (dto.VideoFile != null)
            {
                if (!_allowedVideoExtensions.Contains(Path.GetExtension(dto.VideoFile.FileName).ToLower()))
                    return BadRequest("Chỉ hỗ trợ định dạng video: .mp4, .mov, .avi, .mkv");

                exercise.VideoUrl = await _firebaseStorageService.UploadFileAsync(dto.VideoFile, "exercise_videos");
                updatedProperties.Add(nameof(exercise.VideoUrl));
            }

            // Nếu không có gì thay đổi, trả về lỗi
            if (!updatedProperties.Any())
                return BadRequest("Không có tệp nào được tải lên");

            await _exerciseService.UpdateExerciseAsync(exercise, updatedProperties);
            return Ok(new { exercise.ImageUrl, exercise.VideoUrl });
        }


        [HttpDelete("{id}")]
      
        public async Task<IActionResult> Delete(int id)
        {
            await _exerciseService.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}
