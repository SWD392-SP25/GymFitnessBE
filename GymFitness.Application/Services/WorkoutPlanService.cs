using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class WorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _repository;

        public WorkoutPlanService(IWorkoutPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WorkoutPlan>> GetAllWorkoutPlansAsync() =>
            await _repository.GetAllAsync();

        public async Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddWorkoutPlanAsync(WorkoutPlan plan) =>
            await _repository.AddAsync(plan);

        public async Task UpdateWorkoutPlanAsync(WorkoutPlan plan) =>
            await _repository.UpdateAsync(plan);

        public async Task DeleteWorkoutPlanAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
