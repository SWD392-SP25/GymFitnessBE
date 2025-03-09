using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllAsync(string? filterOn,
                                             string? filterQuery,
                                             string? sortBy,
                                             bool? isAscending,
                                             int pageNumber = 1,
                                             int pageSize = 10);
        Task<Staff?> GetByIdAsync(Guid id);
        Task AddAsync(Staff staff);
        Task UpdateAsync(Staff staff);
        Task DeleteAsync(Guid id);

        Task<Staff?> GetByEmailAsync(string email);
    }
}
