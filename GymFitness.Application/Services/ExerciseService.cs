using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exercise;

        public ExerciseService(IExerciseRepository exercise)
        {
            _exercise = exercise;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync() => await _exercise.GetAllAsync();
        public async Task<Exercise?> GetExerciseByIdAsync(int id) => await _exercise.GetByIdAsync(id);
        public async Task AddExerciseAsync(Exercise exercise) => await _exercise.AddAsync(exercise);
        public async Task UpdateExerciseAsync(Exercise exercise) => await _exercise.UpdateAsync(exercise);
        public async Task DeleteExerciseAsync(int id) => await _exercise.DeleteAsync(id);
    }
}
