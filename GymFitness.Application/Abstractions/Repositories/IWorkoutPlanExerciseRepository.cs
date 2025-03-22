using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IWorkoutPlanExerciseRepository
    {
        Task AddAsync(WorkoutPlanExercise input);
        Task DeleteAsync(int workoutPlanId);
        Task UpdateAsync(WorkoutPlanExercise input, List<string> updatedProperties);
        Task<WorkoutPlanExercise?> GetWorkoutPlanExerciseByIdAsync(int workoutPlanId);
        Task<List<WorkoutPlanExercise>> GetWorkoutPlanExercisesAsync(string? filterOn,
                                                    string? filterQuery,
                                                    string sortBy = "difficulty",
                                                    bool? isAscending = true,
                                                    int pageNumber = 1,
                                                    int pageSize = 10);
    }
}
