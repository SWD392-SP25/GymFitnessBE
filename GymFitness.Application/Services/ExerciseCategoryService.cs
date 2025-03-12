using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class ExerciseCategoryService : IExerciseCategoryService
    {
        private readonly IExerciseCategoryRepository _exerciseCategory;

        public ExerciseCategoryService(IExerciseCategoryRepository exerciseCategory)
        {
            _exerciseCategory = exerciseCategory;
        }

        public async Task<IEnumerable<ExerciseCategoryResponseDto>> GetAllCategoriesAsync(string? filterOn,
                                                                               string? filterQuery,
                                                                               int pageNumber = 1,
                                                                               int pageSize = 10)
        {
            var categories = await _exerciseCategory.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);
            return categories.Select(c => new ExerciseCategoryResponseDto
            {
                Description = c.Description,
                CategoryName = c.Name
            }).ToList();
        }
        public async Task<ExerciseCategory?> GetCategoryByIdAsync(int id) => await _exerciseCategory.GetByIdAsync(id);
        public async Task AddCategoryAsync(ExerciseCategory category) => await _exerciseCategory.AddAsync(category);
        public async Task<ExerciseCategoryResponseDto?> UpdateCategoryAsync(ExerciseCategory category)
        {
            var updatedCategory = await _exerciseCategory.UpdateAsync(category);
            return updatedCategory == null ? null : new ExerciseCategoryResponseDto
            {
                CategoryId = updatedCategory.CategoryId,
                CategoryName = updatedCategory.Name,
                Description = updatedCategory.Description
            };

        }
        public async Task DeleteCategoryAsync(int id) => await _exerciseCategory.DeleteAsync(id);
    }
}
