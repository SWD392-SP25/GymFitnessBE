
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsAsync(string? filterOn,
                                                                       string? filterQuery,
                                                                       string? sortBy,
                                                                       bool? isAscending,
                                                                       int pageNumber = 1,
                                                                       int pageSize = 10);
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment, List<string> updatedProperties);
        Task DeleteAppointmentAsync(int appointmentId);
    }
}
