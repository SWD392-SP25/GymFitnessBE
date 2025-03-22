using GymFitness.Application.ResponseDto;
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
        Task<IEnumerable<SubscriptionPlanResponseDto>> GetSubscriptionPlan(string? filterOn, string? filterQuery, string sortBy = "price", bool? isAscending = true, int pageNumber = 1, int pageSize = 10);

        Task<SubscriptionPlan> GetSubscriptionPlanById(int id);
        Task CreateSubscriptionPlan(SubscriptionPlan input);
        Task UpdateSubscriptionPlan(SubscriptionPlan input, List<string> updatedProperties);
        Task DeleteSubscriptionPlan(int id);
    }
}
