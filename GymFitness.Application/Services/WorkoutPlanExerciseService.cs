using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class WorkoutPlanExerciseService : IWorkoutPlanExerciseService
    {
        private readonly IWorkoutPlanExerciseRepository _workoutPlanExercise;

        public WorkoutPlanExerciseService(IWorkoutPlanExerciseRepository workoutPlanExercise)
        {
            _workoutPlanExercise = workoutPlanExercise;
        }

        public async Task<IEnumerable<WorkoutPlanExercise>> GetAllWorkoutPlanExercisesAsync() =>
            await _workoutPlanExercise.GetAllAsync();

        public async Task<WorkoutPlanExercise?> GetWorkoutPlanExerciseByIdAsync(int planId, int exerciseId) =>
            await _workoutPlanExercise.GetByIdAsync(planId, exerciseId);

        public async Task AddWorkoutPlanExerciseAsync(WorkoutPlanExercise exercise) =>
            await _workoutPlanExercise.AddAsync(exercise);

        public async Task UpdateWorkoutPlanExerciseAsync(WorkoutPlanExercise exercise) =>
            await _workoutPlanExercise.UpdateAsync(exercise);

        public async Task DeleteWorkoutPlanExerciseAsync(int planId, int exerciseId) =>
            await _workoutPlanExercise.DeleteAsync(planId, exerciseId);
    }
}
