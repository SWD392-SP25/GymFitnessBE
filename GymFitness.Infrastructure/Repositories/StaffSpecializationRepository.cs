using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class StaffSpecializationRepository : IStaffSpecializationRepository
    {
        private readonly GymbotDbContext _context;

        public StaffSpecializationRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffSpecialization>> GetAllAsync() =>
            await _context.StaffSpecializations.ToListAsync();

        public async Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId) =>
            await _context.StaffSpecializations
                .FirstOrDefaultAsync(s => s.StaffId == staffId && s.SpecializationId == specializationId);

        public async Task AddAsync(StaffSpecialization staffSpecialization)
        {
            _context.StaffSpecializations.Add(staffSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StaffSpecialization staffSpecialization)
        {
            _context.StaffSpecializations.Update(staffSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid staffId, int specializationId)
        {
            var staffSpecialization = await _context.StaffSpecializations
                .FirstOrDefaultAsync(s => s.StaffId == staffId && s.SpecializationId == specializationId);

            if (staffSpecialization != null)
            {
                _context.StaffSpecializations.Remove(staffSpecialization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
