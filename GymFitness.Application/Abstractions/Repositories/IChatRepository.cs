using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatHistory>> GetChatHistory(Guid senderId, Guid receiverId);
        Task<ChatHistory> SaveMessage(ChatHistory message);
    }
}
