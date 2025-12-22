// 21/12/2025 - 16:29:02
// DANGTHUY

using System.Security.Claims;
using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Services.Auth.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, IEnumerable<string> roles); // cài roles vào accesstoken
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken);
}