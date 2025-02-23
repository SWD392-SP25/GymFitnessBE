using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class AppointmentTypeService
    {
        private readonly IAppointmentTypeRepository _repository;

        public AppointmentTypeService(IAppointmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppointmentType>> GetAllAppointmentTypesAsync() =>
            await _repository.GetAllAsync();

        public async Task<AppointmentType?> GetAppointmentTypeByIdAsync(int typeId) =>
            await _repository.GetByIdAsync(typeId);

        public async Task AddAppointmentTypeAsync(AppointmentType appointmentType) =>
            await _repository.AddAsync(appointmentType);

        public async Task UpdateAppointmentTypeAsync(AppointmentType appointmentType) =>
            await _repository.UpdateAsync(appointmentType);

        public async Task DeleteAppointmentTypeAsync(int typeId) =>
            await _repository.DeleteAsync(typeId);
    }
}
