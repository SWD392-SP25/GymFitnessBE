using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class MuscleGroupService
    {
        private readonly IMuscleGroupRepository _repository;

        public MuscleGroupService(IMuscleGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MuscleGroup>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<MuscleGroup?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(MuscleGroup muscleGroup) => await _repository.AddAsync(muscleGroup);
        public async Task UpdateAsync(MuscleGroup muscleGroup) => await _repository.UpdateAsync(muscleGroup);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
