using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffSpecializationService
    {
        private readonly IStaffSpecializationRepository _repository;

        public StaffSpecializationService(IStaffSpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StaffSpecialization>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId) =>
            await _repository.GetByIdAsync(staffId, specializationId);

        public async Task AddAsync(StaffSpecialization staffSpecialization) =>
            await _repository.AddAsync(staffSpecialization);

        public async Task UpdateAsync(StaffSpecialization staffSpecialization) =>
            await _repository.UpdateAsync(staffSpecialization);

        public async Task DeleteAsync(Guid staffId, int specializationId) =>
            await _repository.DeleteAsync(staffId, specializationId);
    }
}
