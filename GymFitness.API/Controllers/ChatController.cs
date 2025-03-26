using GymFitness.API.Hubs;
using GymFitness.API.RequestDto;
using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IStaffService _staffService;
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatController(IChatService chatService, IStaffService staffService, IUserService userService, IHubContext<ChatHub> chatHub)
        {
            _chatService = chatService;
            _staffService = staffService;
            _userService = userService;
            _chatHub = chatHub;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageRequest request)
        {
            // Kiểm tra sender là User hay Staff
            var userSender = await _userService.GetUserById(request.SenderId);
            var staffSender = await _staffService.GetStaffByIdAsync(request.SenderId);

            if (userSender == null && staffSender == null)
            {
                return BadRequest("SenderId không hợp lệ.");
            }

            // Kiểm tra receiver có tồn tại không
            var userReceiver = await _userService.GetUserById(request.ReceiverId);
            var staffReceiver = await _staffService.GetStaffByIdAsync(request.ReceiverId);

            if (userReceiver == null && staffReceiver == null)
            {
                return BadRequest("ReceiverId không hợp lệ.");
            }

            // Xác định UserId và StaffId
            Guid? userId = userSender != null ? request.SenderId : request.ReceiverId;
            Guid? staffId = staffSender != null ? request.SenderId : request.ReceiverId;

            var chatMessage = new ChatHistory
            {
                UserId = userId,
                StaffId = staffId,
                MessageText = request.Message,
                MessageType = request.MessageType,
                IsUserMessage = userSender != null,  // Nếu sender là User thì true, ngược lại false
                CreatedAt = DateTime.UtcNow
            };

            await _chatService.AddMessageAsync(chatMessage);
            // Gửi tin nhắn real-time qua SignalR
            await _chatHub.Clients.User(request.ReceiverId.ToString())
                .SendAsync("ReceiveMessage", request.SenderId, request.Message, request.MessageType);
            return Ok(new { Message = "Tin nhắn đã được gửi." });
        }

        [HttpGet("history/{userId}/{staffId}")]
        public async Task<IActionResult> GetChatHistory(Guid userId, Guid staffId)
        {
            var messages = await _chatService.GetChatHistoryAsync(userId, staffId);
            return Ok(messages);
        }

    }
}
