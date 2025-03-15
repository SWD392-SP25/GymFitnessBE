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
    class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public UserSubscriptionService(IUserSubscriptionRepository userSubscriptionRepository)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
        }
        public Task AddUserSubcription(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserSubscription(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }

        public Task<UserSubscription?> GetUserSubscriptionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserSubscription>> GetUserSubscriptions(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserSubscription(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
