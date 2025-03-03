using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staff;

        public StaffService(IStaffRepository staff)
        {
            _staff = staff;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffsAsync() =>
            await _staff.GetAllAsync();

        public async Task<Staff?> GetStaffByIdAsync(Guid id) =>
            await _staff.GetByIdAsync(id);

        public async Task AddStaffAsync(Staff staff) =>
            await _staff.AddAsync(staff);

        public async Task UpdateStaffAsync(Staff staff) =>
            await _staff.UpdateAsync(staff);

        public async Task DeleteStaffAsync(Guid id) =>
            await _staff.DeleteAsync(id);

        public async Task<Staff?> GetByEmailAsync(string email) =>
            await _staff.GetByEmailAsync(email);
    }
}
