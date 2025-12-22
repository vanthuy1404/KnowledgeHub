// 16/12/2025 - 22:50:48
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class ChatSession : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Title { get; set; }

    public ICollection<ChatMessage> Messages { get; set; }
}