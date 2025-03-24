using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly GymbotDbContext _context;

        public ChatRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatHistory>> GetChatHistory(Guid senderId, Guid receiverId)
        {
            return await _context.ChatHistories
                .Where(m => (m.UserId == senderId && m.StaffId == receiverId) || 
                            (m.UserId == receiverId && m.StaffId == senderId))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<ChatHistory> SaveMessage(ChatHistory message)
        {
            _context.ChatHistories.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
