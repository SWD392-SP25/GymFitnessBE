using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IStaffSpecializationService
    {
        Task<IEnumerable<StaffSpecializationResponseDto>> GetAllAsync(string? filterOn,
                                                           string? filterQuery,
                                                           string? sortBy,
                                                           bool? isAscending,
                                                           int pageNumber = 1,
                                                           int pageSize = 10);

        Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId);
        Task AddAsync(StaffSpecialization staffSpecialization);
        Task UpdateAsync(StaffSpecialization staffSpecialization);
        Task DeleteAsync(Guid staffId, int specializationId);
    }
}
