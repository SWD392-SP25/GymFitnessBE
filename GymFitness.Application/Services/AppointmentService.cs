using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
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


         public async Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {

            // Gọi repository để lấy dữ liệu đã được lọc và phân trang
            var appointments = await _appointment.GetAppointmentsAsync(
                filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Chuyển đổi từ Entity -> DTO trước khi trả về Controller
            return appointments.Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                UserName = a.User.Email,
                StaffName = a.Staff.Email,
                Status = a.Status,
                Notes = a.Notes,
                Location = a.Location,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Description = a.Type != null ? a.Type.Description : null,
                CreatedAt = a.CreatedAt
            }).ToList();


    }
       


        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            var existingAppointment = _appointment.GetByIdAsync(appointmentId);
            if (existingAppointment == null)
                return null;
            return await existingAppointment;
        }
            

        public async Task AddAppointmentAsync(Appointment appointment) =>
            await _appointment.AddAsync(appointment);

        public async Task UpdateAppointmentAsync(Appointment appointment) =>
            await _appointment.UpdateAsync(appointment);

        public async Task DeleteAppointmentAsync(int appointmentId) =>
            await _appointment.DeleteAsync(appointmentId);

        
    }
}
