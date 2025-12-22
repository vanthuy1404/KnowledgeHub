// 21/12/2025 - 17:11:32
// DANGTHUY

using KnowledgeHub.Data;
using KnowledgeHub.Data.Entities.Auth;
using KnowledgeHub.Repository.Base;
using KnowledgeHub.Repository.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Repository.Users.Implementations;

public class UserRepository :BaseRepository<User>, IUserRepository
{
    public UserRepository(KnowledgeHubDbContext context)
        : base(context)
    {
    }
    public override async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _dbSet
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.UserName == userName);
    }

    public async Task AddAsync(User user)
    {
        await base.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await base.UpdateAsync(user);
    }
}