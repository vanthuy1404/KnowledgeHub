// 16/12/2025 - 22:48:59
// DANGTHUY

namespace KnowledgeHub.Data.Entities.Auth;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Document> Documents { get; set; }
    public ICollection<SearchQuery> SearchQueries { get; set; }
    public ICollection<ChatSession> ChatSessions { get; set; }
    public ICollection<EventLog> EventLogs { get; set; }
    public ICollection<UserActivity> UserActivities { get; set; }
}