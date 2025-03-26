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
        Task<List<WorkoutPlanResponseDto>> GetWorkoutPlans(string? filterOn, 
                                                           string? filterQuery, 
                                                           string sortBy = "difficulty", 
                                                           bool? isAscending = true, 
                                                           int pageNumber = 1, 
                                                           int pageSize = 10);
        Task<WorkoutPlan?> GetWorkoutPlanById(int workoutPlanId);
        Task AddWorkoutPlan(WorkoutPlan workoutPlan);
        Task UpdateWorkoutPlan(WorkoutPlan workoutPlan, List<string> updatedProperties);
        Task<List<WorkoutPlan?>> GetWorkoutPlansByStaff(Guid staffId);
        Task DeleteWorkoutPlan(int workoutPlanId);
        Task<List<WorkoutPlan?>> GetWorkoutPlansBySubscriptionPlanId(int subscriptionPlanId);
    }
}
