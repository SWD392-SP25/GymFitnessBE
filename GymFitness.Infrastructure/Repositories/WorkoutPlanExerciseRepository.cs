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
    public class WorkoutPlanExerciseRepository : IWorkoutPlanExerciseRepository
    {
        private readonly GymbotDbContext _context;

        public WorkoutPlanExerciseRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(WorkoutPlanExercise input)
        {
            await _context.WorkoutPlanExercises.AddAsync(input);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int workoutPlanId)
        {
            var isExist = await  GetWorkoutPlanExerciseByIdAsync(workoutPlanId);
            if (isExist != null)
            {
                _context.WorkoutPlanExercises.Remove(isExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WorkoutPlanExercise?> GetWorkoutPlanExerciseByIdAsync(int workoutPlanId)
        {
            return await _context.WorkoutPlanExercises.Include(x => x.Exercise)
                                                      .ThenInclude(x => x.MuscleGroup)
                                                      .Include(x => x.Exercise)
                                                      .ThenInclude(x => x.Category)
                                                      .FirstOrDefaultAsync(x => x.PlanId.Equals(workoutPlanId));
        }

        public Task<List<WorkoutPlanExercise>> GetWorkoutPlanExercisesAsync(string? filterOn = null, string? filterQuery = null, string sortBy = "difficulty", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var workoutPlanExercises = _context.WorkoutPlanExercises.Include(x => x.Exercise)
                                                                    .ThenInclude(x => x.MuscleGroup)
                                                                    .Include(x => x.Exercise)
                                                                    .ThenInclude(x => x.Category)
                                                                    .AsQueryable();

            Console.WriteLine($"Raw data from Repo: {workoutPlanExercises.ToList().Count}");
            foreach (var wpe in workoutPlanExercises)
            {
                Console.WriteLine($"PlanId: {wpe.PlanId}, ExerciseId: {wpe.Exercise?.ExerciseId ?? -1}");
            }



            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                workoutPlanExercises = filterOn switch
                {
                    "name" => workoutPlanExercises.Where(x => x.Exercise.Name != null && x.Exercise.Name.ToLower().Contains(filterQuery)),
                    "description" => workoutPlanExercises.Where(x => x.Exercise.Description != null && x.Exercise.Description.ToString().Contains(filterQuery)),
                    "difficulty" => workoutPlanExercises.Where(x => x.Exercise.DifficultyLevel != null && x.Exercise.DifficultyLevel.ToString().Contains(filterQuery)),
                    _ => workoutPlanExercises
                };
            }
            Console.WriteLine($"Workout plans exercise found at Repo after filtering: {workoutPlanExercises.ToList().Count}");
            // **Sắp xếp**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.ToLower();
                workoutPlanExercises = sortBy switch
                {
                    
                    "difficulty" => isAscending.Value ? workoutPlanExercises.OrderBy(x => x.Exercise.DifficultyLevel) : workoutPlanExercises.OrderByDescending(x => x.Exercise.DifficultyLevel),
                    _ => workoutPlanExercises
                };
            }
            Console.WriteLine($"Workout plans exercise found at Repo after sorting: {workoutPlanExercises.ToList().Count}");
            // **Phân trang**
            return workoutPlanExercises.Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize).ToListAsync();
        }

        public async Task UpdateAsync(WorkoutPlanExercise input, List<string> updatedProperties)
        {
            var entry = _context.Entry(input);

            foreach (var property in updatedProperties)
            {
                if(_context.Entry(input).Properties.Any(x => x.Metadata.Name == property))
                {
                    entry.Property(property).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
