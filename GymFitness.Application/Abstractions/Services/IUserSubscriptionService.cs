using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IUserSubscriptionService
    {
        Task AddUserSubcription(UserSubscription userSubscription);
        Task UpdateUserSubscription(UserSubscription userSubscription);
        Task DeleteUserSubscription(UserSubscription userSubscription);
        Task<UserSubscriptionResponseDto?> GetUserSubscriptionById(int id);
        Task<IEnumerable<UserSubscriptionResponseDto>> GetUserSubscriptions(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10);
    }
}
