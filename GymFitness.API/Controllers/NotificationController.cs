using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequestDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.DeviceToken))
                return BadRequest("Invalid request.");

            var notification = new RegisteredDevice
            {
                UserId = request.UserId,
                StaffId = request.StaffId,
                Title = request.Title,
                Message = request.Message,
                Type = request.Type
            };

            bool isSent = await _notificationService.SendNotificationAsync(notification, request.DeviceToken);

            if (isSent)
                return Ok(new { message = "Notification sent successfully." });

            return StatusCode(500, "Failed to send notification.");
        }
    }

    
}
