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

        public async Task<List<ChatHistory>> GetChatHistoryAsync(Guid userId, Guid staffId)
        {
         
        return await _context.ChatHistories
            .Where(m => (m.UserId == userId && m.StaffId == staffId))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
        }

        public async Task AddMessageAsync(ChatHistory chatMessage)
        {
            await _context.ChatHistories.AddAsync(chatMessage);
            await _context.SaveChangesAsync();
        }
    }
}
