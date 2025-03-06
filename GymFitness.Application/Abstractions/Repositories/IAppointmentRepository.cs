using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointmentsAsync(string? filterOn, 
                                                            string? filterQuery, 
                                                            string? sortBy, 
                                                            bool isAscending, 
                                                            int pageNumber = 1, 
                                                            int pageSize = 10);
        Task<Appointment?> GetByIdAsync(int appointmentId);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int appointmentId);
    }
}
