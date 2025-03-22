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
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly GymbotDbContext _context;

        public SubscriptionPlanRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task Add(SubscriptionPlan input)
        {
            await _context.SubscriptionPlans.AddAsync(input);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var isExist = await GetSubscriptionPlanById(id);
            if(isExist != null)
            {
                isExist.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SubscriptionPlan>> GetSubscriptionPlan(string? filterOn, string? filterQuery, string sortBy = "price", bool? isAscending = true , int pageNumber = 1, int pageSize = 10)
        {
            var subscriptionPlan = _context.SubscriptionPlans
                                                
                                                    
                                                    .AsQueryable();

            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                subscriptionPlan = filterOn switch
                {
                    "name" => subscriptionPlan.Where(x => x.Name != null && x.Name.ToLower().Contains(filterQuery)),
                    "description" => subscriptionPlan.Where(x => x.Description != null && x.Description.ToString().Contains(filterQuery)),
                    "features" => subscriptionPlan.Where(x => x.Features != null && x.Features.ToLower().Contains(filterQuery)),

                    _ => subscriptionPlan
                };
            }

            // **Sắp xếp**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.ToLower();
                subscriptionPlan = sortBy switch
                {
                    //"name" => isAscending.Value ? subscriptionPlan.OrderBy(x => x.Name) : subscriptionPlan.OrderByDescending(x => x.Name),
                    "price" => isAscending.Value ? subscriptionPlan.OrderBy(x => x.Price) : subscriptionPlan.OrderByDescending(x => x.Price),
                    _ => subscriptionPlan
                };
            }

            // **Phân trang**
            subscriptionPlan = subscriptionPlan.Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize);
            return await subscriptionPlan.ToListAsync();

        }

        public async Task<SubscriptionPlan?> GetSubscriptionPlanById(int id)
        {
            return await _context.SubscriptionPlans
                                            .FirstOrDefaultAsync(x => x.SubscriptionPlanId.Equals(id));
        }

        public async Task Update(SubscriptionPlan input, List<string> updatedProperties)
        {
            var entry = _context.Entry(input);
            foreach (var property in updatedProperties)
            {
                if(_context.Entry(input).Properties.Any(p => p.Metadata.Name == property))
                {
                    entry.Property(property).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
