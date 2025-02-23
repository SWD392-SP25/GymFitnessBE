using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IStaffSpecializationRepository
    {
        Task<IEnumerable<StaffSpecialization>> GetAllAsync();
        Task<StaffSpecialization?> GetByIdAsync(int staffId, int specializationId);
        Task AddAsync(StaffSpecialization staffSpecialization);
        Task UpdateAsync(StaffSpecialization staffSpecialization);
        Task DeleteAsync(int staffId, int specializationId);
    }
}
