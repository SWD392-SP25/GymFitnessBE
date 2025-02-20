using GymFitness.Infrastructure.Data;

namespace GymFitness.API.Services.Abstractions
{
    public interface IStaffService
    {
        Task<Staff> GetStaffByEmail(string email);
        void AddStaff(Staff staff);
    }
}
