// 16/12/2025 - 22:50:54
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class ChatMessage : BaseEntity
{
    public Guid SessionId { get; set; }
    public ChatSession Session { get; set; }

    public string Role { get; set; }
    public string Content { get; set; }
    public int? TokenUsage { get; set; }

    public ICollection<ChatMessageSource> Sources { get; set; }
}