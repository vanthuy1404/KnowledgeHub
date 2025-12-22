// 16/12/2025 - 23:00:32
// DANGTHUY

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeHub.Data.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDbContextExtension (this IServiceCollection serviceCollection,
        string connectionString)
    {
        serviceCollection.AddDbContext<KnowledgeHubDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        return serviceCollection;
    }
}