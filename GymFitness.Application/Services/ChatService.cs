using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        public async Task<IEnumerable<ChatHistory>> GetChatHistory(Guid senderId, Guid receiverId)
        {
            return await _chatRepository.GetChatHistory(senderId, receiverId);
        }

     

        public Task<ChatHistory> SendMessage(int senderId, int receiverId, string messageText, bool isUserMessage)
        {
            return null;
            //var message = new ChatHistory
            //{
            //    StaffId = senderId,
            //    ReceiverId = receiverId,
            //    MessageText = messageText,
            //    IsUserMessage = isUserMessage,
            //    CreatedAt = DateTime.UtcNow
            //};

            //return await _chatRepository.SaveMessage(message);
        }
    }
}
