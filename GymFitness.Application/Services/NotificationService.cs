
using FirebaseAdmin.Messaging;
using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> SendNotificationAsync(GymFitness.Domain.Entities.Notification notification, string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
                return false;

            var message = new Message()
            {
                Token = deviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = notification.Title,
                    Body = notification.Message
                },
                Data = new Dictionary<string, string>
                {
                    { "type", notification.Type ?? "general" },
                    { "createdAt", DateTime.UtcNow.ToString("o") }
                }
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"✅ Notification sent successfully: {response}");

                // Gán thêm thông tin trước khi lưu vào database
                notification.CreatedAt = DateTime.UtcNow;
                notification.IsRead = false;

                await _notificationRepository.AddNotificationAsync(notification);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error sending notification: {ex.Message}");
                return false;
            }
        }
    }
}
