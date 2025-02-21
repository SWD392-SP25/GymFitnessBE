

using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GymFitness.Application.Services;
public class SubscriptionPlanService
{
    private readonly ISubscriptionPlanRepository _repository;

    public SubscriptionPlanService(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubscriptionPlan>> GetAllPlansAsync() => await _repository.GetAllAsync();
    public async Task<SubscriptionPlan?> GetPlanByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddPlanAsync(SubscriptionPlan plan) => await _repository.AddAsync(plan);
    public async Task UpdatePlanAsync(SubscriptionPlan plan) => await _repository.UpdateAsync(plan);
    public async Task DeletePlanAsync(int id) => await _repository.DeleteAsync(id);
}
