namespace GymFitness.Domain.Abstractions.Services
{
    public interface IChatCompletionService
    {
        Task<string> GetChatCompletionAsync(string question);
    }
} 