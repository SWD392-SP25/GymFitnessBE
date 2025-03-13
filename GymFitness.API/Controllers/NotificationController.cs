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
        private readonly IDeviceTokenService _deviceTokenService;

        public NotificationController(INotificationService notificationService, IDeviceTokenService deviceTokenService)
        {
            _notificationService = notificationService;
            _deviceTokenService = deviceTokenService;
        }

        [HttpPost("sendindividual")]
        public async Task<IActionResult> SendIndividualNotification([FromBody] NotificationRequestDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.DeviceToken))
                return BadRequest("Invalid request.");

            var notification = new RegisteredDevice
            {
                //UserId = request.UserId,
                //StaffId = request.StaffId,
                Title = request.Title,
                Message = request.Message,
                Type = request.Type
            };

            bool isSent = await _notificationService.SendNotificationAsync(notification, request.DeviceToken);

            if (isSent)
                return Ok(new { message = "Notification sent successfully." });

            return StatusCode(500, "Failed to send notification.");
        }

        [HttpPost("sendall")]
        public async Task<IActionResult> SendAllNotification([FromBody] NotificationRequestDto request)
        {
            if (request == null)
                return BadRequest("Invalid request.");

            // Lấy tất cả device tokens từ DeviceTokenService
            var deviceTokens = await _deviceTokenService.GetDeviceTokensAsync();

            if (deviceTokens == null || !deviceTokens.Any())
                return BadRequest("No registered device tokens found.");

            var notification = new RegisteredDevice
            {
                //UserId = request.UserId,
                //StaffId = request.StaffId,
                Title = request.Title,
                Message = request.Message,
                Type = request.Type
            };

            // Gửi thông báo đến từng device token
            var sendResults = new List<bool>();
            foreach (var token in deviceTokens)
            {
                bool isSent = await _notificationService.SendNotificationAsync(notification, token);
                sendResults.Add(isSent);
            }

            if (sendResults.All(result => result))
                return Ok(new { message = "Notification sent successfully to all devices." });

            return StatusCode(500, "Failed to send notification to some devices.");
        }

    }


}
