using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class WorkoutPlanExerciseService
    {
        private readonly IWorkoutPlanExerciseRepository _repository;

        public WorkoutPlanExerciseService(IWorkoutPlanExerciseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WorkoutPlanExercise>> GetAllWorkoutPlanExercisesAsync() =>
            await _repository.GetAllAsync();

        public async Task<WorkoutPlanExercise?> GetWorkoutPlanExerciseByIdAsync(int planId, int exerciseId) =>
            await _repository.GetByIdAsync(planId, exerciseId);

        public async Task AddWorkoutPlanExerciseAsync(WorkoutPlanExercise exercise) =>
            await _repository.AddAsync(exercise);

        public async Task UpdateWorkoutPlanExerciseAsync(WorkoutPlanExercise exercise) =>
            await _repository.UpdateAsync(exercise);

        public async Task DeleteWorkoutPlanExerciseAsync(int planId, int exerciseId) =>
            await _repository.DeleteAsync(planId, exerciseId);
    }
}
