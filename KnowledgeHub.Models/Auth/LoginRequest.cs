// 21/12/2025 - 16:48:57
// DANGTHUY

namespace KnowledgeHub.Models.Auth;

public record LoginRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
}