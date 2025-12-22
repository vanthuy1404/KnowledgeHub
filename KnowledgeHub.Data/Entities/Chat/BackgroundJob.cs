// 16/12/2025 - 22:51:42
// DANGTHUY

namespace KnowledgeHub.Data.Entities;

public class BackgroundJob : BaseEntity
{
    public string JobType { get; set; }
    public string Payload { get; set; }
    public string Status { get; set; }
    public int RetryCount { get; set; }
}