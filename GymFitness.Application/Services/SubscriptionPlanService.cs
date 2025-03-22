using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
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

        public async Task CreateSubscriptionPlan(SubscriptionPlan input)
        {
            await _subscriptionPlanRepository.Add(input);
        }

        public async Task DeleteSubscriptionPlan(int id)
        {
            await _subscriptionPlanRepository.Delete(id);
        }

        public async Task<IEnumerable<SubscriptionPlanResponseDto>> GetSubscriptionPlan(string? filterOn, string? filterQuery, string sortBy = "price", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var subscriptionPlan = await _subscriptionPlanRepository.GetSubscriptionPlan(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return subscriptionPlan.Select(a => new SubscriptionPlanResponseDto
            {
                SubscriptionPlanId = a.SubscriptionPlanId,
                CreatedAt = a.CreatedAt,
                Description = a.Description,

            });
        }

        public async Task<SubscriptionPlan> GetSubscriptionPlanById(int id)
        {
            return await _subscriptionPlanRepository.GetSubscriptionPlanById(id);
        }

        public Task UpdateSubscriptionPlan(SubscriptionPlan input, List<string> updatedProperties)
        {
            throw new NotImplementedException();
        }

      
    }
}
