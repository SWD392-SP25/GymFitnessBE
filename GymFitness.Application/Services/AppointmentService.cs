using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync() =>
            await _repository.GetAllAsync();

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId) =>
            await _repository.GetByIdAsync(appointmentId);

        public async Task AddAppointmentAsync(Appointment appointment) =>
            await _repository.AddAsync(appointment);

        public async Task UpdateAppointmentAsync(Appointment appointment) =>
            await _repository.UpdateAsync(appointment);

        public async Task DeleteAppointmentAsync(int appointmentId) =>
            await _repository.DeleteAsync(appointmentId);
    }
}
