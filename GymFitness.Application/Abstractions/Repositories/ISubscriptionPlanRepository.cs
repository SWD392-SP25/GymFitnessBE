using GymFitness.Infrastructure.Data;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface ISubscriptionPlanRepository
    {
        Task<IEnumerable<SubscriptionPlan>> GetAllAsync();
        Task<SubscriptionPlan?> GetByIdAsync(int id);
        Task AddAsync(SubscriptionPlan subscriptionPlan);
        Task UpdateAsync(SubscriptionPlan subscriptionPlan);
        Task DeleteAsync(int id);
    }
}
