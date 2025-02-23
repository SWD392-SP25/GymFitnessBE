namespace GymFitness.API.Dto
{
    public class MuscleGroupDto
    {
        public int? MuscleGroupId { get; set; }  // Nullable, không cần khi tạo mới
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
