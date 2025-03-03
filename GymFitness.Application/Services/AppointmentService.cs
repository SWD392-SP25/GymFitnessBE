using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointment;

        public AppointmentService(IAppointmentRepository appointment)
        {
            _appointment = appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync() =>
           await _appointment.GetAllAsync();

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId) =>
            await _appointment.GetByIdAsync(appointmentId);

        public async Task AddAppointmentAsync(Appointment appointment) =>
            await _appointment.AddAsync(appointment);

        public async Task UpdateAppointmentAsync(Appointment appointment) =>
            await _appointment.UpdateAsync(appointment);

        public async Task DeleteAppointmentAsync(int appointmentId) =>
            await _appointment.DeleteAsync(appointmentId);
    }
}
