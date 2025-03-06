namespace GymFitness.API.Dto
{
    public class CreateMuscleGroupRequestDto
    {
        public int? MuscleGroupId { get; set; }  // Nullable, không cần khi tạo mới
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
