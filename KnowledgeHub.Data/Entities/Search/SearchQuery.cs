// 16/12/2025 - 22:50:31
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Data.Entities;

public class SearchQuery : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string QueryText { get; set; }

    public ICollection<SearchResult> Results { get; set; }
}