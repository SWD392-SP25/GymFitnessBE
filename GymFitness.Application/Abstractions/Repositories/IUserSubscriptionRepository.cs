using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IUserSubscriptionRepository
    {
        Task<List<UserSubscription>> GetUserSubscriptions(string? filterOn,
                                                          string? filterQuery,
                                                          int pageNumber = 1,
                                                          int pageSize = 10);
        Task<UserSubscription?> GetUserSubscriptionById(int id);
        Task AddUserSubcription(UserSubscription userSubscription);
        Task UpdateUserSubscription(UserSubscription userSubscription);
        Task DeleteUserSubscription(UserSubscription userSubscription);
    }
}
