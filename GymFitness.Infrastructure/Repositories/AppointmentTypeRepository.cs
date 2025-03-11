using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class AppointmentTypeRepository : IAppointmentTypeRepository
    {
        private readonly GymbotDbContext _context;

        public AppointmentTypeRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentType>> GetAllAsync(string? filterOn, 
                                                                    string? filterQuery,
                                                                    int pageNumber = 1,
                                                                    int pageSize = 10)
        {
            var appointmentTypes = _context.AppointmentTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                appointmentTypes = filterOn switch
                {
                    "name" => appointmentTypes.Where(a => EF.Functions.Like(a.Name, filterQuery)),
                    "description" => appointmentTypes.Where(a => EF.Functions.Like(a.Description, filterQuery)),
                    _ => appointmentTypes
                };
            }

            // **Phân trang**
            appointmentTypes = appointmentTypes.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await appointmentTypes.ToListAsync();
        }
            

        public async Task<AppointmentType?> GetByIdAsync(int typeId) =>
            await _context.AppointmentTypes.FindAsync(typeId);

        public async Task AddAsync(AppointmentType appointmentType)
        {
            _context.AppointmentTypes.Add(appointmentType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppointmentType appointmentType)
        {
            _context.AppointmentTypes.Update(appointmentType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int typeId)
        {
            var entity = await _context.AppointmentTypes.FindAsync(typeId);
            if (entity != null)
            {
                _context.AppointmentTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


    }
}
