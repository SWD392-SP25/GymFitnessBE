using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _workoutPlan;

        public WorkoutPlanService(IWorkoutPlanRepository workoutPlan)
        {
            _workoutPlan = workoutPlan;
        }

        public async Task<IEnumerable<WorkoutPlan>> GetAllWorkoutPlansAsync() =>
            await _workoutPlan.GetAllAsync();

        public async Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int id) =>
            await _workoutPlan.GetByIdAsync(id);

        public async Task AddWorkoutPlanAsync(WorkoutPlan plan) =>
            await _workoutPlan.AddAsync(plan);

        public async Task UpdateWorkoutPlanAsync(WorkoutPlan plan) =>
            await _workoutPlan.UpdateAsync(plan);

        public async Task DeleteWorkoutPlanAsync(int id) =>
            await _workoutPlan.DeleteAsync(id);
    }
}
