// 16/12/2025 - 22:49:40
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class Document : BaseEntity
{
    public string Title { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public long FileSize { get; set; }
    public string StorageProvider { get; set; }
    public string StoragePath { get; set; }
    public string Checksum { get; set; }
    public string Status { get; set; }

    public Guid CreatedBy { get; set; }
    public User Creator { get; set; }

    public ICollection<DocumentVersion> Versions { get; set; }
    public ICollection<DocumentChunk> Chunks { get; set; }
}