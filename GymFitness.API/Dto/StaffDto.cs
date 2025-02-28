namespace GymFitness.API.Dto
{
    public class StaffDto
    {
        public Guid StaffId { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public string? Phone { get; set; }
        public decimal? Salary { get; set; }
        public string? Status { get; set; }
        public string? Department { get; set; }
    }
}
