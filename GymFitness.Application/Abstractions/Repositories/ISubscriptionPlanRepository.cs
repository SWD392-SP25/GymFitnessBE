using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface ISubscriptionPlanRepository
    {
        Task<IEnumerable<SubscriptionPlan>> GetSubscriptionPlan(string? filterOn,
                                                   string? filterQuery,
                                                   string sortBy = "price",
                                                   bool? isAscending = true,
                                                   int pageNumber = 1,
                                                   int pageSize = 10);

        Task<SubscriptionPlan?> GetSubscriptionPlanById(int id);
        Task Add(SubscriptionPlan input);
        Task Update(SubscriptionPlan input, List<string> updatedProperties);
        Task Delete(int id);

    }
}
