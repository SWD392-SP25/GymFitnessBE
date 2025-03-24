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
        Task<IEnumerable<ChatHistory>> GetChatHistory(Guid senderId, Guid receiverId);
        Task<ChatHistory> SendMessage(int senderId, int receiverId, string messageText, bool isUserMessage);
    }
}
