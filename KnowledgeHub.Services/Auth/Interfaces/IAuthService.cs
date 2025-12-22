// 21/12/2025 - 16:47:09
// DANGTHUY

using KnowledgeHub.Models.Auth;

namespace KnowledgeHub.Services.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RefreshAsync(string refreshToken, string accessToken);
}