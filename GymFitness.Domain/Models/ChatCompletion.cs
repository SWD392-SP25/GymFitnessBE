namespace GymFitness.Domain.Models
{
    public class ChatCompletionRequest
    {
        public string Model { get; set; } = string.Empty;
        public int MaxTokens { get; set; }
        public List<Message> Messages { get; set; } = [];
    }

    public class Message
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class ChatCompletionResponse
    {
        public List<Choice>? Choices { get; set; }
    }

    public class Choice
    {
        public Message? Message { get; set; }
    }
} 