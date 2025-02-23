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
    public class MuscleGroupController : ControllerBase
    {
        private readonly MuscleGroupService _service;

        public MuscleGroupController(MuscleGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var muscleGroups = await _service.GetAllAsync();
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
        public async Task<IActionResult> Create([FromBody] MuscleGroupDto dto)
        {
            var muscleGroup = new MuscleGroup
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            };

            await _service.AddAsync(muscleGroup);
            return CreatedAtAction(nameof(GetById), new { id = muscleGroup.MuscleGroupId }, muscleGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MuscleGroupDto dto)
        {
            var muscleGroup = await _service.GetByIdAsync(id);
            if (muscleGroup == null)
                return NotFound();

            muscleGroup.Name = dto.Name;
            muscleGroup.Description = dto.Description;
            muscleGroup.ImageUrl = dto.ImageUrl;

            await _service.UpdateAsync(muscleGroup);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
