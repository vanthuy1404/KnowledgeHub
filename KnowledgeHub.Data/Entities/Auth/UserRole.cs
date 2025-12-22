// 16/12/2025 - 22:49:33
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}