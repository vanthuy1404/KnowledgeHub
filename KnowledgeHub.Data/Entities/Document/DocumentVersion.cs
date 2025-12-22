// 16/12/2025 - 22:49:50
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class DocumentVersion : BaseEntity
{
    public Guid DocumentId { get; set; }
    public Document Document { get; set; }

    public int VersionNumber { get; set; }
    public string StoragePath { get; set; }
    public string Checksum { get; set; }

    public Guid CreatedBy { get; set; }
    public User Creator { get; set; }
}