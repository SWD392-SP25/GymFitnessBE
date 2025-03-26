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
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly IWorkoutPlanRepository _workoutPlanRepository;


        public ChatService(IChatRepository chatRepository, IUserSubscriptionRepository userSubscriptionRepository, IWorkoutPlanRepository workoutPlanRepository)
        {
            _chatRepository = chatRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
            _workoutPlanRepository = workoutPlanRepository;
        }

        public async Task AddMessageAsync(ChatHistory chatMessage)
        {
            await _chatRepository.AddMessageAsync(chatMessage);
        }


        public async Task<List<ChatHistory>> GetChatHistoryAsync(Guid userId, Guid staffId)
        {
            return await _chatRepository.GetChatHistoryAsync(userId, staffId);
        }
    }
}
