// 16/12/2025 - 22:48:12
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}