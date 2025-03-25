using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GymFitness.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IChatService _chatService;

        public ChatHub(IUserService userService, IStaffService staffService, IChatService chatService)
        {
            _userService = userService;
            _staffService = staffService;
            _chatService = chatService;
        }

        public async Task SendMessage(Guid senderId, Guid receiverId, string message, string messageType)
        {
            // Xác định người gửi là User hay Staff
            bool isUserSender = await _userService.GetUserById(senderId) != null;
            bool isStaffSender = await _staffService.GetStaffByIdAsync(senderId) != null;

            if (!isUserSender && !isStaffSender)
            {
                throw new HubException("Người gửi không hợp lệ.");
            }

            Guid? userId = isUserSender ? senderId : receiverId;
            Guid? staffId = isStaffSender ? senderId : receiverId;

            var chatMessage = new ChatHistory
            {
                UserId = userId,
                StaffId = staffId,
                MessageText = message,
                MessageType = messageType,
                IsUserMessage = isUserSender,
                CreatedAt = DateTime.UtcNow
            };

            await _chatService.AddMessageAsync(chatMessage);

            // Gửi tin nhắn real-time đến người nhận qua SignalR
            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", chatMessage);
        }
    }
}
