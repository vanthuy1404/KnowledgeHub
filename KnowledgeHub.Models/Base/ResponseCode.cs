// 21/12/2025 - 16:55:49
// DANGTHUY

namespace KnowledgeHub.Models.Base;
public static class ResponseCode
{
    // ===== Success =====
    public const string Success = "SUCCESS";

    // ===== Info =====
    public const string Info = "INFO";
    public const string NoData = "NO_DATA";

    // ===== Validation =====
    public const string ValidationError = "VALIDATION_ERROR";

    // ===== Auth =====
    public const string AuthInvalidCredentials = "AUTH_INVALID_CREDENTIALS";
    public const string AuthUserInactive = "AUTH_USER_INACTIVE";
    public const string AuthUnauthorized = "AUTH_UNAUTHORIZED";
    public const string AuthTokenInvalid = "AUTH_TOKEN_INVALID";
    public const string AuthTokenExpired = "AUTH_TOKEN_EXPIRED";

    // ===== Permission =====
    public const string Forbidden = "FORBIDDEN";

    // ===== System =====
    public const string NotFound = "NOT_FOUND";
    public const string InternalServerError = "INTERNAL_SERVER_ERROR";
}
