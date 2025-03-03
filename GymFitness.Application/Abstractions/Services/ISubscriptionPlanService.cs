using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface ISubscriptionPlanService
    {
        Task<IEnumerable<SubscriptionPlan>> GetAllPlansAsync();
        Task<SubscriptionPlan?> GetPlanByIdAsync(int id);
        Task AddPlanAsync(SubscriptionPlan plan);
        Task UpdatePlanAsync(SubscriptionPlan plan);
        Task DeletePlanAsync(int id);
    }
}
