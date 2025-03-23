using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IWorkoutPlanRepository
    {
        Task<List<WorkoutPlan>> GetWorkoutPlansAsync(string? filterOn,
                                                    string? filterQuery,
                                                    string sortBy = "difficulty",
                                                    bool? isAscending = true,
                                                    int pageNumber = 1,
                                                    int pageSize = 10);
        Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int workoutPlanId);
        Task AddAsync(WorkoutPlan workoutPlan);
        Task UpdateAsync(WorkoutPlan workoutPlan, List<string> updatedProperties);
        Task DeleteAsync(int workoutPlanId);
        Task<List<WorkoutPlan?>> GetWorkoutPlansBySubscriptionPlanIdAsync(int subscriptionPlanId);
    }
}
