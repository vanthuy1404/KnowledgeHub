// 21/12/2025 - 16:47:20
// DANGTHUY

using System.IdentityModel.Tokens.Jwt;
using KnowledgeHub.Models.Auth;

using System.Security.Claims;
using KnowledgeHub.Data.Entities.Auth;
using KnowledgeHub.Models.Auth;
using KnowledgeHub.Models.Base;
using KnowledgeHub.Repository;
using KnowledgeHub.Repository.Users.Interfaces;
using KnowledgeHub.Services.Auth;
using KnowledgeHub.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(
        IUserRepository userRepo,
        ITokenService tokenService,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    // =========================
    // LOGIN
    // =========================
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepo.GetByUserNameAsync(request.UserName);

        // ❗ cố tình gộp để không leak thông tin
        if (user == null || !user.IsActive)
            throw new AuthException(
                ResponseCode.AuthInvalidCredentials,
                "Invalid username or password");

        var verifyResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (verifyResult == PasswordVerificationResult.Failed)
            throw new AuthException(
                ResponseCode.AuthInvalidCredentials,
                "Invalid username or password");

        var roles = user.UserRoles?.Select(x => x.Role.Name) ?? Enumerable.Empty<string>();

        var accessToken = _tokenService.GenerateAccessToken(user, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.LastLoginAt = DateTime.UtcNow;

        await _userRepo.UpdateAsync(user);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiredAt = DateTime.UtcNow.AddMinutes(15)
        };
    }

    // =========================
    // REFRESH TOKEN
    // =========================
    public async Task<LoginResponse> RefreshAsync(string refreshToken, string accessToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
            throw new AuthException(
                ResponseCode.AuthTokenInvalid,
                "Invalid token");

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrEmpty(userId))
            throw new AuthException(
                ResponseCode.AuthTokenInvalid,
                "Invalid token");

        var user = await _userRepo.GetByIdAsync(Guid.Parse(userId));
        if (user == null || !user.IsActive)
            throw new AuthException(
                ResponseCode.AuthUnauthorized,
                "Unauthorized");

        if (user.RefreshToken != refreshToken)
            throw new AuthException(
                ResponseCode.AuthTokenInvalid,
                "Invalid token");

        if (user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new AuthException(
                ResponseCode.AuthTokenExpired,
                "Token expired");

        // 🔁 rotate refresh token
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var roles = user.UserRoles?.Select(x => x.Role.Name) ?? Enumerable.Empty<string>();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _userRepo.UpdateAsync(user);

        return new LoginResponse
        {
            AccessToken = _tokenService.GenerateAccessToken(user, roles),
            RefreshToken = newRefreshToken,
            AccessTokenExpiredAt = DateTime.UtcNow.AddMinutes(15)
        };
    }
}