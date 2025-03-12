﻿namespace GymFitness.API.Dto
{
    public class ExercisePatchDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? MuscleGroupId { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyLevel { get; set; }
        public string? EquipmentNeeded { get; set; }
        public string? Instructions { get; set; }
        public string? Precautions { get; set; }
    }

    public class ExerciseFileDto
    {
        public IFormFile? ImageFile { get; set; }
        public IFormFile? VideoFile { get; set; }
    }

}
