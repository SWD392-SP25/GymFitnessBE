using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
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

        public async Task<IEnumerable<ExerciseCategory>> GetAllCategoriesAsync() => await _exerciseCategory.GetAllAsync();
        public async Task<ExerciseCategory?> GetCategoryByIdAsync(int id) => await _exerciseCategory.GetByIdAsync(id);
        public async Task AddCategoryAsync(ExerciseCategory category) => await _exerciseCategory.AddAsync(category);
        public async Task UpdateCategoryAsync(ExerciseCategory category) => await _exerciseCategory.UpdateAsync(category);
        public async Task DeleteCategoryAsync(int id) => await _exerciseCategory.DeleteAsync(id);
    }
}
