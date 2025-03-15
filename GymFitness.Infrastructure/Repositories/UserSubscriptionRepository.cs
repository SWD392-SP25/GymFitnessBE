using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly GymbotDbContext _context;

        public UserSubscriptionRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public async Task AddUserSubcription(UserSubscription userSubscription)
        {
            await _context.UserSubscriptions.AddAsync(userSubscription);
            await _context.SaveChangesAsync();
        }

        public Task DeleteUserSubscription(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSubscription?> GetUserSubscriptionById(int id)
        {
            return await _context.UserSubscriptions.Include(x => x.SubscriptionPlan)
                                                   .FirstOrDefaultAsync(x => x.SubscriptionId == id);
        }

        public Task<List<UserSubscription>> GetUserSubscriptions(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var userSubscriptions = _context.UserSubscriptions.Include(x => x.SubscriptionPlan)
                                                              .AsQueryable();

            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                userSubscriptions = filterOn switch
                {
                    "email" => userSubscriptions.Where(x => x.User.Email != null && x.User.Email.ToLower().Contains(filterQuery)),
                    "status" => userSubscriptions.Where(x => x.Status != null && x.Status.ToLower().Contains(filterQuery)),
                    "location" => userSubscriptions.Where(x => x.SubscriptionPlan != null && x.SubscriptionPlan.Name.ToLower().Contains(filterQuery)),
                    _ => userSubscriptions
                };
            }

            // **Phân trang**
            userSubscriptions = userSubscriptions.Skip((pageNumber - 1) * pageSize)
                                                 .Take(pageSize);
            return userSubscriptions.ToListAsync();
        }

        public async Task UpdateUserSubscription(UserSubscription userSubscription)
        {
            var existingData = await GetUserSubscriptionById(userSubscription.SubscriptionId);
            if (existingData != null)
            {
                _context.Entry(existingData).CurrentValues.SetValues(userSubscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
