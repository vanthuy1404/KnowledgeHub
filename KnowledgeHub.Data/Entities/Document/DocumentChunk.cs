// 16/12/2025 - 22:50:15
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class DocumentChunk : BaseEntity
{
    public Guid DocumentId { get; set; }
    public Document Document { get; set; }

    public int VersionNumber { get; set; }
    public int ChunkIndex { get; set; }
    public string Content { get; set; }
    public int? TokenCount { get; set; }

    public DocumentEmbedding Embedding { get; set; }
    public ICollection<SearchResult> SearchResults { get; set; }
    public ICollection<ChatMessageSource> ChatMessageSources { get; set; }
}