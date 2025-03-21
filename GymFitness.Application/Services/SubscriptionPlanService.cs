using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;

        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }
        public async Task<IEnumerable<SubscriptionPlan>> GetSubscriptionPlan(string? filterOn, string? filterQuery, string sortBy = "price", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            return await _subscriptionPlanRepository.GetSubscriptionPlan(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
        }

        public async Task<SubscriptionPlan?> GetSubscriptionPlanById(int id)
        {
            return await _subscriptionPlanRepository.GetSubscriptionPlanById(id);
        }
    }
}
