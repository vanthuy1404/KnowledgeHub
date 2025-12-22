// 16/12/2025 - 22:49:22
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}