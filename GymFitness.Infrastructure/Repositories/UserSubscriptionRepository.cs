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
    public class UserSubscriptionRepository : IUserSubscriptionRepository
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

        public async Task DeleteUserSubscription(UserSubscription userSubscription)
        {
            _context.UserSubscriptions.Remove(userSubscription);
            await _context.SaveChangesAsync();
        }

        public async Task<UserSubscription?> GetUserSubscriptionById(int id)
        {
            return await _context.UserSubscriptions.Include(x => x.SubscriptionPlan)
                                                   .Include(x => x.Invoices)
                                                   .FirstOrDefaultAsync(x => x.SubscriptionId == id);
        }

        public async Task<List<UserSubscription>> GetUserSubscriptions(string? filterOn, string? filterQuery, int pageNumber = 1, int pageSize = 10)
        {
            var userSubscriptions = _context.UserSubscriptions.Include(x => x.SubscriptionPlan)
                                                              .Include(x => x.Invoices)
                                                              .AsQueryable();

            var userSubscriptionCount = await _context.UserSubscriptions.Include(x => x.SubscriptionPlan)
                                                                        .Include(x => x.Invoices)
                                                                        .ToListAsync();

            Console.WriteLine($"Số lượng UserSubscriptions: {userSubscriptionCount.Count}");

            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                userSubscriptions = filterOn switch
                {
                    "email" => userSubscriptions.Where(x => x.User.Email != null && x.User.Email.ToLower().Contains(filterQuery)),
                    "status" => userSubscriptions.Where(x => x.Status != null && x.Status.ToLower().Contains(filterQuery)),
                    //"location" => userSubscriptions.Where(x => x.SubscriptionPlan != null && x.SubscriptionPlan.Name.ToLower().Contains(filterQuery)),
                    _ => userSubscriptions
                };
            }

            // **Phân trang**
            userSubscriptions = userSubscriptions.Skip((pageNumber - 1) * pageSize)
                                                 .Take(pageSize);
            return await userSubscriptions.ToListAsync();
        }

        public async Task UpdateUserSubscription(UserSubscription userSubscription, List<string> updatedProperties)
        {
            var entry = _context.Entry(userSubscription);

            foreach (var property in updatedProperties)
            {
                if (_context.Entry(userSubscription).Properties.Any(p => p.Metadata.Name == property))
                {
                    entry.Property(property).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();

        }
    }
}
