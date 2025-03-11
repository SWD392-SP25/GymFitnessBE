using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IWorkoutPlanService
    {
        Task<IEnumerable<WorkoutPlanResponseDto>> GetAllWorkoutPlansAsync(string? filterOn,
                                                               string? filterQuery,
                                                               string? sortBy,
                                                               bool? isAscending,
                                                               int pageNumber = 1,
                                                               int pageSize = 10);
        Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int id);
        Task AddWorkoutPlanAsync(WorkoutPlan plan);
        Task UpdateWorkoutPlanAsync(WorkoutPlan plan);
        Task DeleteWorkoutPlanAsync(int id);
    }
}
