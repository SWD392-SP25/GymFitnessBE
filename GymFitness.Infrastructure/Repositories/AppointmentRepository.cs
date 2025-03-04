using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly GymbotDbContext _context;

        public AppointmentRepository(GymbotDbContext context)
        {
            _context = context;
        }

        

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var appointments = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Staff)
                .Include(a => a.Type)
                .AsQueryable();

            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();

                appointments = filterOn switch
                {
                    "email" => appointments.Where(a => a.User != null && a.User.Email.ToLower().Contains(filterQuery)),
                    "status" => appointments.Where(a => a.Status != null && a.Status.ToLower().Contains(filterQuery)),
                    _ => appointments
                };
            }

            // **Sắp xếp linh hoạt theo `sortBy`**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.ToLower();
                appointments = sortBy switch
                {
                    "email" => isAscending
                        ? appointments.OrderBy(a => a.User != null ? a.User.Email : "")
                        : appointments.OrderByDescending(a => a.User != null ? a.User.Email : ""),

                    "status" => isAscending
                        ? appointments.OrderBy(a => a.Status)
                        : appointments.OrderByDescending(a => a.Status),

                    "starttime" => isAscending
                        ? appointments.OrderBy(a => a.StartTime)
                        : appointments.OrderByDescending(a => a.StartTime),

                    "appointmentid" => isAscending
                        ? appointments.OrderBy(a => a.AppointmentId)
                        : appointments.OrderByDescending(a => a.AppointmentId),

                    _ => appointments
                };
            }

            // **Phân trang**
            appointments = appointments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await appointments.ToListAsync();
        }


        public async Task<Appointment?> GetByIdAsync(int appointmentId) =>
            await _context.Appointments
                          .Include(x => x.User)
                          .Include(x => x.Staff)
                          .Include(x => x.Type)
                          .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);


        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int appointmentId)
        {
            var entity = await _context.Appointments.FindAsync(appointmentId);
            if (entity != null)
            {
                _context.Appointments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


    }
}
