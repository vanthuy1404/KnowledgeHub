// 16/12/2025 - 22:50:25
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class DocumentEmbedding : BaseEntity
{
    public Guid ChunkId { get; set; }
    public DocumentChunk Chunk { get; set; }

    public float[] Embedding { get; set; }
    public string EmbeddingModel { get; set; }
    public int Dimension { get; set; }
}