// 21/12/2025 - 17:04:45
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;

namespace KnowledgeHub.Repository.Users.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUserNameAsync(string userName);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}