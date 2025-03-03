using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffSpecializationService : IStaffSpecializationService
    {
        private readonly IStaffSpecializationRepository _staffSpecialization;

        public StaffSpecializationService(IStaffSpecializationRepository staffSpecialization)
        {
            _staffSpecialization = staffSpecialization;
        }

        public async Task<IEnumerable<StaffSpecialization>> GetAllAsync() =>
            await _staffSpecialization.GetAllAsync();

        public async Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId) =>
            await _staffSpecialization.GetByIdAsync(staffId, specializationId);

        public async Task AddAsync(StaffSpecialization staffSpecialization) =>
            await _staffSpecialization.AddAsync(staffSpecialization);

        public async Task UpdateAsync(StaffSpecialization staffSpecialization) =>
            await _staffSpecialization.UpdateAsync(staffSpecialization);

        public async Task DeleteAsync(Guid staffId, int specializationId) =>
            await _staffSpecialization.DeleteAsync(staffId, specializationId);
    }
}
