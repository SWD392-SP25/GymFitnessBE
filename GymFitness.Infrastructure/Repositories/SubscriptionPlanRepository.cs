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
        public async Task<IEnumerable<SubscriptionPlan>> GetSubscriptionPlan(string? filterOn, string? filterQuery, string sortBy = "price", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var subscriptionPlan = _context.SubscriptionPlans
                                                    .Include(x => x.WorkoutPlans)
                                                    .ThenInclude(x => x.WorkoutPlanExercises)
                                                    .ThenInclude(x => x.Exercise)
                                                    .ThenInclude(x => x.MuscleGroup)
                                                    .Include(x => x.WorkoutPlans)
                                                    .ThenInclude(x => x.WorkoutPlanExercises)
                                                    .ThenInclude(x => x.Exercise)
                                                    .ThenInclude(x => x.Category) // Bổ sung Include ExerciseCategory
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
                    "musclegroup" => subscriptionPlan.Where(x =>
                    x.WorkoutPlans.Any(x => 
                    x.WorkoutPlanExercises.Any(x =>
                    x.Exercise.MuscleGroup.Name.ToLower().Contains(filterQuery)))),

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
    }
}
