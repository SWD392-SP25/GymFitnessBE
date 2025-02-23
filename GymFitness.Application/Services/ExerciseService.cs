using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class ExerciseService
    {
        private readonly IExerciseRepository _repository;

        public ExerciseService(IExerciseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync() => await _repository.GetAllAsync();
        public async Task<Exercise?> GetExerciseByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddExerciseAsync(Exercise exercise) => await _repository.AddAsync(exercise);
        public async Task UpdateExerciseAsync(Exercise exercise) => await _repository.UpdateAsync(exercise);
        public async Task DeleteExerciseAsync(int id) => await _repository.DeleteAsync(id);
    }
}
