using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IWorkoutPlanRepository
    {
        Task<IEnumerable<WorkoutPlan>> GetAllAsync(string? filterOn,
                                                   string? filterQuery,
                                                   string? sortBy,
                                                   bool? isAscending,
                                                   int pageNumber = 1,
                                                   int pageSize = 10);
        Task<WorkoutPlan?> GetByIdAsync(int id);
        Task AddAsync(WorkoutPlan workoutPlan);
        Task UpdateAsync(WorkoutPlan workoutPlan);
        Task DeleteAsync(int id);
    }
}
