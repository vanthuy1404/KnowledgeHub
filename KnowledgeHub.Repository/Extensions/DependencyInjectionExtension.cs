// 21/12/2025 - 18:10:44
// DANGTHUY

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeHub.Repository.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var implementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

        foreach (var implementation in implementations)
        {
            var interfaceVariable = implementation.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implementation.Name}");

            if (interfaceVariable != null)
            {
                services.AddScoped(interfaceVariable, implementation);
            }
        }

        return services;
    }
}