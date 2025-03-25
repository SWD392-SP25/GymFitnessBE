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

        public Task AddMessageAsync(ChatHistory chatMessage)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChatHistory>> GetChatHistoryAsync(Guid userId, Guid staffId)
        {
            throw new NotImplementedException();
        }
    }
}
