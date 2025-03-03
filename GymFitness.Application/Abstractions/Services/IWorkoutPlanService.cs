using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IWorkoutPlanService
    {
        Task<IEnumerable<WorkoutPlan>> GetAllWorkoutPlansAsync();
        Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int id);
        Task AddWorkoutPlanAsync(WorkoutPlan plan);
        Task UpdateWorkoutPlanAsync(WorkoutPlan plan);
        Task DeleteWorkoutPlanAsync(int id);
    }
}
