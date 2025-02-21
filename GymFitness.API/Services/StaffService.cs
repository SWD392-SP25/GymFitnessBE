using GymFitness.API.Services.Abstractions;
using GymFitness.Infrastructure.Data;
using GymFitness.Infrastructure.Repositories.Abstractions;

namespace GymFitness.API.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public async Task<Staff> GetStaffByEmail(string email)
        {
            return await _staffRepository.GetStaffByEmail(email);
        }

        public async void AddStaff(Staff staff)
        {
            _staffRepository.AddStaff(staff);
        }

    }
}
