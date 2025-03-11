using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseCategoryController : ControllerBase
    {
        private readonly IExerciseCategoryService _service;

        public ExerciseCategoryController(IExerciseCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var categories = await _service.GetAllCategoriesAsync(filterOn, filterQuery, pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExerciseCategoryDto dto)
        {
            var category = new ExerciseCategory { Name = dto.Name, Description = dto.Description };
            await _service.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExerciseCategoryDto dto)
        {
            var existingCategory = await _service.GetCategoryByIdAsync(id);
            if (existingCategory == null) return NotFound();

            existingCategory.Name = dto.Name;
            existingCategory.Description = dto.Description;

            await _service.UpdateCategoryAsync(existingCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
