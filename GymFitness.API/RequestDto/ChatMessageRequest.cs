namespace GymFitness.API.RequestDto
{
    public class ChatMessageRequest
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; } 
    }
}
