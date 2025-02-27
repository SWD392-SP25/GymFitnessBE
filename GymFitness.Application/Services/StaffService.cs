using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffsAsync() =>
            await _staffRepository.GetAllAsync();

        public async Task<Staff?> GetStaffByIdAsync(Guid id) =>
            await _staffRepository.GetByIdAsync(id);

        public async Task AddStaffAsync(Staff staff) =>
            await _staffRepository.AddAsync(staff);

        public async Task UpdateStaffAsync(Staff staff) =>
            await _staffRepository.UpdateAsync(staff);

        public async Task DeleteStaffAsync(Guid id) =>
            await _staffRepository.DeleteAsync(id);
    }
}
