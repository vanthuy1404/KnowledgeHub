// 21/12/2025 - 16:48:48
// DANGTHUY

namespace KnowledgeHub.Models.Auth;

public record LoginResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public DateTime AccessTokenExpiredAt { get; init; }
}