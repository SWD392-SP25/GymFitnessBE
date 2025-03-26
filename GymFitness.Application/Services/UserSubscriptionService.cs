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
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly IInvoicesRepository _invoicesRepository;

        public UserSubscriptionService(IUserSubscriptionRepository userSubscriptionRepository, IInvoicesRepository invoicesRepository)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
            _invoicesRepository = invoicesRepository;
        }
        public async Task AddUserSubcription(UserSubscription userSubscription)
        {
            await _userSubscriptionRepository.AddUserSubcription(userSubscription);
        }

        public async Task DeleteUserSubscription(UserSubscription userSubscription)
        {
            await _userSubscriptionRepository.DeleteUserSubscription(userSubscription);
        }

        public async Task<UserSubscription?> GetPendingSubscriptionBySubscriptionId(int subscriptionId)
        {
            return await _userSubscriptionRepository.GetPendingSubscriptionBySubscriptionId(subscriptionId);
        }

        public async Task<List<UserSubscriptionBuyDto>> GetUsersByStaffAsync(Guid staffId)
        {
            var userSubscriptions = await  _userSubscriptionRepository.GetUsersByStaffAsync(staffId);

            return userSubscriptions.Select(us => new UserSubscriptionBuyDto
            {
                UserId = us.UserId ?? Guid.Empty,
                UserEmail = us.User?.Email ?? "N/A",
                StartDate = us.StartDate,
                EndDate = us.EndDate,
                SubscriptionPlanName = us.SubscriptionPlan?.Name ?? "Unknown"
            }).ToList();
        }

        public async Task<UserSubscription?> GetUserSubscriptionById(int id)
        {
            var userSubscription = await _userSubscriptionRepository.GetUserSubscriptionById(id);

            if (userSubscription == null) return null;

            return userSubscription;
        }

        public async Task<IEnumerable<UserSubscriptionResponseDto>> GetUserSubscriptions(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var userSubscriptions = await _userSubscriptionRepository.GetUserSubscriptions(filterOn, filterQuery, pageNumber, pageSize);
            

            return  userSubscriptions.Select(x => new UserSubscriptionResponseDto
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
                Invoices = x.Invoices?.Select(invoice => new InvoiceResponseDto
                {
                    InvoiceId = invoice.InvoiceId,
                    Amount = invoice.Amount,
                    Status = invoice.Status,
                    DueDate = invoice.DueDate,
                    PaidDate = invoice.PaidDate,
                    PaymentMethod = invoice.PaymentMethod?.MethodName,
                    CreatedAt = invoice.CreatedAt
                }).ToList() ?? new List<InvoiceResponseDto>() // Tránh null list
            }).ToList();

        }

        public async Task UpdateUserSubscription(UserSubscription userSubscription, List<string> updatedProperties)
        {
            await _userSubscriptionRepository.UpdateUserSubscription(userSubscription, updatedProperties);
        }
    }
}
