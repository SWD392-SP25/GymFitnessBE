using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class WorkoutPlanExerciseResponseDto
    {
        public int WorkoutPlanId { get; set; }
        public int WeekNumber { get; set; }

        public int DayOfWeek { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }

        public int? RestTimeSeconds { get; set; }

        public string? Notes { get; set; }
 

        public ExerciseResponseDto Exercise { get; set; } = null!;
    }
}
