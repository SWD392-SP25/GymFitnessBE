using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<WorkoutPlan>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.WorkoutPlans.Include(x => x.CreatedByNavigation)
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
                query = filterOn switch
                {
                    "planname" => query.Where(a => a.Name.ToLower().Contains(filterQuery)),
                    "description" => query.Where(a => a.Description.ToLower().Contains(filterQuery)),
                    "createdby" => query.Where(a => a.CreatedBy != null && a.CreatedByNavigation.Email.ToLower().Contains(filterQuery)),
                    "staffname" => query.Where(a => a.CreatedByNavigation.LastName.ToLower().Contains(filterQuery)),
                    _ => query
                };
            }
            // **Sắp xếp**
            //if (!string.IsNullOrWhiteSpace(sortBy))
            //{
            //    sortBy = sortBy.ToLower();

            //    if (sortBy == "difficultylevel")
            //    {
            //        query = (isAscending == true)
            //            ? query.AsEnumerable() // Chuyển về LINQ-to-Objects
            //                   .OrderBy(a => int.TryParse(a.DifficultyLevel, out int level) ? level : int.MaxValue)
            //                   .AsQueryable() // Chuyển về IQueryable nếu cần
            //            : query.AsEnumerable()
            //                   .OrderByDescending(a => int.TryParse(a.DifficultyLevel, out int level) ? level : int.MaxValue)
            //                   .AsQueryable();
            //    }
            //    else
            //    {
            //        query = sortBy switch
            //        {
            //            "name" => isAscending == true
            //                        ? query.OrderBy(a => a.Name)
            //                        : query.OrderByDescending(a => a.Name),
            //            _ => query
            //        };
            //    }
            //}
            //if (!string.IsNullOrWhiteSpace(sortBy))
            //{
            //    sortBy = sortBy.ToLower();

            //    if (sortBy == "difficultylevel")
            //    {
            //        query = isAscending == true
            //            ? query.OrderBy(a => EF.Functions.Like(a.DifficultyLevel, "%[^0-9]%") ? int.MaxValue : int.Parse(a.DifficultyLevel))
            //            : query.OrderByDescending(a => EF.Functions.Like(a.DifficultyLevel, "%[^0-9]%") ? int.MaxValue : int.Parse(a.DifficultyLevel));
            //    }
            //    else
            //    {
            //        query = sortBy switch
            //        {
            //            "name" => isAscending == true
            //                        ? query.OrderBy(a => a.Name)
            //                        : query.OrderByDescending(a => a.Name),
            //            _ => query
            //        };
            //    }
            //}

            // **Phân trang**
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();


        }
        //public async Task<IEnumerable<WorkoutPlan>> GetAllAsync() =>
        //    await _context.WorkoutPlans.ToListAsync();

        public async Task<WorkoutPlan?> GetByIdAsync(int id) =>
            await _context.WorkoutPlans.FindAsync(id);

        public async Task AddAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Update(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan != null)
            {
                _context.WorkoutPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }


    }
}
