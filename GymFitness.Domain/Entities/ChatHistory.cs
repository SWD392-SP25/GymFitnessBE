using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class ChatHistory
{
    public int ChatId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? StaffId { get; set; }

    public string MessageText { get; set; } = null!;

    public string? MessageType { get; set; }

    public bool IsUserMessage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual User? User { get; set; }
}
