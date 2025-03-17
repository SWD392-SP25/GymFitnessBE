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
        Task<IEnumerable<SubscriptionPlan>> GetSubscriptionPlan(string? filterOn, string? filterQuery, string sortBy = "price", bool? isAscending = true, int pageNumber = 1, int pageSize = 10);
    }
}
