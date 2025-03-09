using GymFitness.Domain.Entities;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IStaffSpecializationRepository
    {
        Task<IEnumerable<StaffSpecialization>> GetAllAsync(string? filterOn,
                                                           string? filterQuery,
                                                           string? sortBy,
                                                           bool? isAscending,
                                                           int? pageNumber = 1,
                                                           int? pageSize = 10);
        Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId);
        Task AddAsync(StaffSpecialization staffSpecialization);
        Task UpdateAsync(StaffSpecialization staffSpecialization);
        Task DeleteAsync(Guid staffId, int specializationId);
    }
}
