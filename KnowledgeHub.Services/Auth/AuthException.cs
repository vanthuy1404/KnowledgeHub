// 21/12/2025 - 17:02:12
// DANGTHUY

namespace KnowledgeHub.Services.Auth;

public class AuthException : Exception
{
    public string Code { get; }

    public AuthException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}