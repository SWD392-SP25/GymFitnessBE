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
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private readonly GymbotDbContext _context;

        public WorkoutPlanRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(WorkoutPlan workoutPlan)
        {
            await _context.WorkoutPlans.AddAsync(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int workoutPlanId)
        {
            var workoutPlan = await _context.WorkoutPlans
                .Include(wp => wp.WorkoutPlanExercises) // Load danh sách bài tập liên quan
                .FirstOrDefaultAsync(wp => wp.PlanId == workoutPlanId);

            if (workoutPlan != null)
            {
                _context.WorkoutPlanExercises.RemoveRange(workoutPlan.WorkoutPlanExercises); // Xóa tất cả bài tập
                _context.WorkoutPlans.Remove(workoutPlan); // Xóa WorkoutPlan
                await _context.SaveChangesAsync();
            }
        }


        public async Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int workoutPlanId)
        {
            return await _context.WorkoutPlans.Include(x => x.WorkoutPlanExercises)
                                              .FirstOrDefaultAsync(x => x.PlanId.Equals(workoutPlanId));
        }

        public Task<List<WorkoutPlan>> GetWorkoutPlansAsync(string? filterOn, string? filterQuery, string sortBy = "difficulty", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var workoutPlans = _context.WorkoutPlans.Include(x => x.CreatedByNavigation)
                                                    .Include(x => x.WorkoutPlanExercises)
                                                    .ThenInclude(x => x.Exercise)
                                                    .ThenInclude(x => x.MuscleGroup)
                                                    .ThenInclude(x => x.Exercises)
                                                    .ThenInclude(x => x.Category)
                                                    .AsQueryable();



            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                workoutPlans = filterOn switch
                {
                    "name" => workoutPlans.Where(x => x.Name != null && x.Name.ToLower().Contains(filterQuery)),
                    "description" => workoutPlans.Where(x => x.Description != null && x.Description.ToString().Contains(filterQuery)),
                    "difficulty" => workoutPlans.Where(x => x.DifficultyLevel != null && x.DifficultyLevel.ToString().Contains(filterQuery)),
                    _ => workoutPlans
                };
            }

            // **Sắp xếp**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.ToLower();
                workoutPlans = sortBy switch
                {
                   
                    "difficulty" => isAscending.Value ? workoutPlans.OrderBy(x => x.DifficultyLevel) : workoutPlans.OrderByDescending(x => x.DifficultyLevel),
                    _ => workoutPlans
                };
            }

            // **Phân trang**
            workoutPlans = workoutPlans.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize);
            return workoutPlans.ToListAsync();
        }

        public async Task<List<WorkoutPlan?>> GetWorkoutPlansByStaff(Guid staffId)
        {
            return await _context.WorkoutPlans.Where(x => x.CreatedBy.Equals(staffId))
                                              .ToListAsync();
        }

        public async Task<List<WorkoutPlan?>> GetWorkoutPlansBySubscriptionPlanIdAsync(int subscriptionPlanId)
        {
            return await _context.WorkoutPlans.Include(x => x.CreatedByNavigation)
                                              .Include(x => x.WorkoutPlanExercises)
                                              .ThenInclude(x => x.Exercise)
                                              .ThenInclude(x => x.MuscleGroup)
                                              .ThenInclude(x => x.Exercises)
                                              .ThenInclude(x => x.Category)
                                              .Where(x => x.SubscriptionPlanId.Equals(subscriptionPlanId))
                                              .ToListAsync();
        }

        public async Task UpdateAsync(WorkoutPlan workoutPlan, List<string> updatedProperties)
        {
            var entry = _context.Entry(workoutPlan);

            foreach (var property in updatedProperties)
            {
                if (_context.Entry(workoutPlan).Properties.Any(p => p.Metadata.Name == property))
                {
                    entry.Property(property).IsModified = true;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
