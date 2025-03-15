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

        public Task<UserSubscriptionResponseDto?> GetUserSubscriptionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserSubscriptionResponseDto>> GetUserSubscriptions(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var userSubscriptions = await _userSubscriptionRepository.GetUserSubscriptions(filterOn, filterQuery, pageNumber, pageSize);

            return userSubscriptions.Select(x => new UserSubscriptionResponseDto
            {
                SubscriptionId = x.SubscriptionId,
                UserEmail = x.User.Email,
                SubscriptionPlanId = x.SubscriptionPlanId,
                SubscriptionPlanName = x.SubscriptionPlan.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                PaymentFrequency = x.PaymentFrequency,
                AutoRenew = x.AutoRenew,
                CreatedAt = x.CreatedAt,
                Sub = x.Sub,
                InvoiceId = x.Invoices.FirstOrDefault().InvoiceId,
                InvoiceStatus = x.Invoices.FirstOrDefault().Status
            });

        }

        public Task UpdateUserSubscription(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
