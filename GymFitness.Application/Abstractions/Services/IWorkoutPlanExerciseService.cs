using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IWorkoutPlanExerciseService
    {
        Task<IEnumerable<WorkoutPlanExercise>> GetAllWorkoutPlanExercisesAsync();
        Task<WorkoutPlanExercise?> GetWorkoutPlanExerciseByIdAsync(int planId, int exerciseId);
        Task AddWorkoutPlanExerciseAsync(WorkoutPlanExercise exercise);
        Task UpdateWorkoutPlanExerciseAsync(WorkoutPlanExercise exercise);
        Task DeleteWorkoutPlanExerciseAsync(int planId, int exerciseId);
    }
}
