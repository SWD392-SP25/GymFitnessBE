using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IChatService
    {
        Task AddMessageAsync(ChatHistory chatMessage);
        Task<List<ChatHistory>> GetChatHistoryAsync(Guid userId, Guid staffId);
    }
}
