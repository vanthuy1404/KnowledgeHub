// 16/12/2025 - 22:51:06
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class EventLog : BaseEntity
{
    public string EntityType { get; set; }
    public Guid EntityId { get; set; }
    public string Action { get; set; }
    public string Data { get; set; }

    public Guid CreatedBy { get; set; }
    public User Creator { get; set; }
}