// 16/12/2025 - 22:50:35
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class SearchResult : BaseEntity
{
    public Guid SearchQueryId { get; set; }
    public SearchQuery SearchQuery { get; set; }

    public Guid ChunkId { get; set; }
    public DocumentChunk Chunk { get; set; }

    public float Score { get; set; }
    public int Rank { get; set; }
}