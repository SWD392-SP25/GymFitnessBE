using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class ExerciseCategoryService
    {
        private readonly IExerciseCategoryRepository _repository;

        public ExerciseCategoryService(IExerciseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExerciseCategory>> GetAllCategoriesAsync() => await _repository.GetAllAsync();
        public async Task<ExerciseCategory?> GetCategoryByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddCategoryAsync(ExerciseCategory category) => await _repository.AddAsync(category);
        public async Task UpdateCategoryAsync(ExerciseCategory category) => await _repository.UpdateAsync(category);
        public async Task DeleteCategoryAsync(int id) => await _repository.DeleteAsync(id);
    }
}
