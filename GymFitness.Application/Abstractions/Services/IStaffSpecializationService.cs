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
        Task<IEnumerable<StaffSpecialization>> GetAllAsync();

        Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId);
        Task AddAsync(StaffSpecialization staffSpecialization);
        Task UpdateAsync(StaffSpecialization staffSpecialization);
        Task DeleteAsync(Guid staffId, int specializationId);
    }
}
