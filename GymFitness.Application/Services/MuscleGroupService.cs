using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class MuscleGroupService : IMuscleGroupService
    {
        private readonly IMuscleGroupRepository _muscleGroup;

        public MuscleGroupService(IMuscleGroupRepository muscleGroup)
        {
            _muscleGroup = muscleGroup;
        }

        public async Task<IEnumerable<MuscleGroup>> GetAllAsync() => await _muscleGroup.GetAllAsync();
        public async Task<MuscleGroup?> GetByIdAsync(int id) => await _muscleGroup.GetByIdAsync(id);
        public async Task AddAsync(MuscleGroup muscleGroup) => await _muscleGroup.AddAsync(muscleGroup);
        public async Task UpdateAsync(MuscleGroup muscleGroup) => await _muscleGroup.UpdateAsync(muscleGroup);
        public async Task DeleteAsync(int id) => await _muscleGroup.DeleteAsync(id);
    }
}
