using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IWorkoutPlanExerciseRepository
    {
        Task<IEnumerable<WorkoutPlanExercise>> GetAllAsync();
        Task<WorkoutPlanExercise?> GetByIdAsync(int planId, int exerciseId);
        Task AddAsync(WorkoutPlanExercise workoutPlanExercise);
        Task UpdateAsync(WorkoutPlanExercise workoutPlanExercise);
        Task DeleteAsync(int planId, int exerciseId);
    }
}
