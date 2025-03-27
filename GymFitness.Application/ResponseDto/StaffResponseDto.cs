using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class StaffResponseDto
    {
        public Guid StaffId { get; set; }
        public string? Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Role { get; set; }
        public string? Phone { get; set; }
        public DateOnly? HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }
        public decimal? Salary { get; set; }
        public string? Status { get; set; }
        public string? Department { get; set; }
        public Guid? SupervisorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
