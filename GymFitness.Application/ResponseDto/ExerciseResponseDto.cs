using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class ExerciseResponseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? MuscleGroupName { get; set; }
        public string? CategoryName { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? EquipmentNeeded { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? Instructions { get; set; }
        public string? Precautions { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
