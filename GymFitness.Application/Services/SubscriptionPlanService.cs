

using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GymFitness.Application.Services;
public class SubscriptionPlanService : ISubscriptionPlanService
{
    private readonly ISubscriptionPlanRepository _subscriptionPlan;

    public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlan)
    {
        _subscriptionPlan = subscriptionPlan;
    }

    public async Task<IEnumerable<SubscriptionPlan>> GetAllPlansAsync() => await _subscriptionPlan.GetAllAsync();
    public async Task<SubscriptionPlan?> GetPlanByIdAsync(int id) => await _subscriptionPlan.GetByIdAsync(id);
    public async Task AddPlanAsync(SubscriptionPlan plan) => await _subscriptionPlan.AddAsync(plan);
    public async Task UpdatePlanAsync(SubscriptionPlan plan) => await _subscriptionPlan.UpdateAsync(plan);
    public async Task DeletePlanAsync(int id) => await _subscriptionPlan.DeleteAsync(id);
}
