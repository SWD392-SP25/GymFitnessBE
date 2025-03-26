using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MuscleGroupController : ControllerBase
    {
        private readonly IMuscleGroupService _service;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public MuscleGroupController(IMuscleGroupService service, IFirebaseStorageService firebaseStorageService)
        {
            _service = service;
            _firebaseStorageService = firebaseStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var muscleGroups = await _service.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);
            return Ok(muscleGroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var muscleGroup = await _service.GetByIdAsync(id);
            if (muscleGroup == null)
                return NotFound();
            return Ok(muscleGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMuscleGroupRequestDto dto)
        {
            if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            // Upload file lên Firebase Storage
            string imageUrl = await _firebaseStorageService.UploadFileAsync(dto.ImageUrl, "muscle-groups");

            // Tạo đối tượng MuscleGroup
            var muscleGroup = new MuscleGroup
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = imageUrl // Gán URL ảnh từ Firebase
            };

            await _service.AddAsync(muscleGroup);

            return CreatedAtAction(nameof(GetById), new { id = muscleGroup.MuscleGroupId }, muscleGroup);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateMuscleGroupRequestDto dto)
        {
            var muscleGroup = await _service.GetByIdAsync(id);
            if (muscleGroup == null)
                return NotFound();

            muscleGroup.Name = dto.Name;
            muscleGroup.Description = dto.Description;

            // Nếu có ảnh mới, upload lên Firebase và cập nhật URL
            if (dto.ImageUrl != null && dto.ImageUrl.Length > 0)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(muscleGroup.ImageUrl))
                {
                    await _firebaseStorageService.DeleteFileAsync(muscleGroup.ImageUrl);
                }

                // Upload ảnh mới
                string imageUrl = await _firebaseStorageService.UploadFileAsync(dto.ImageUrl, "muscle-groups");
                muscleGroup.ImageUrl = imageUrl;
            }

            await _service.UpdateAsync(muscleGroup);
            return Ok("Update successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
