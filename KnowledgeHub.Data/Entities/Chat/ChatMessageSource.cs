// 16/12/2025 - 22:51:00
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class ChatMessageSource
{
    public Guid MessageId { get; set; }
    public ChatMessage Message { get; set; }

    public Guid ChunkId { get; set; }
    public DocumentChunk Chunk { get; set; }
}