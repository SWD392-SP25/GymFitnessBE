using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class WorkoutExerciseResponseDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = null!;
        
        public int? Sets { get; set; }  // Số set của bài tập
        public int? Reps { get; set; }  // Số reps mỗi set
        public int? RestTime { get; set; }
        public string? Notes { get; set; }

        public string? MuscleGroupName { get; set; }
        public string? CategoryName { get; set; }
        public int? DifficultyLevel { get; set; }
        public string? EquipmentNeeded { get; set; }
        public string? VideoUrl { get; set; }
    }
}
