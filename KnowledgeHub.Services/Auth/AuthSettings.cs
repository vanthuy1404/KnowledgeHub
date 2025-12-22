// 21/12/2025 - 16:28:21
// DANGTHUY

namespace KnowledgeHub.Services.Auth;

public class AuthSettings // Bind to "AuthSettings" section in appsettings.json
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenMinutes { get; set; }
    public int RefreshTokenDays { get; set; }
    public string SecretKey { get; set; }
}