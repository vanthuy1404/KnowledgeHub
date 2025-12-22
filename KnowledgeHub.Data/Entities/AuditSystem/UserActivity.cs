// 16/12/2025 - 22:51:13
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class UserActivity : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Action { get; set; }
    public string TargetType { get; set; }
    public Guid? TargetId { get; set; }
}