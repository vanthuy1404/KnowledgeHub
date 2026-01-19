// 21/12/2025 - 18:13:37
// DANGTHUY

using KnowledgeHub.Data.Entities.Auth;
using KnowledgeHub.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace KnowledgeHub.Services.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var implementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

        foreach (var implementation in implementations)
        {
            var interfaceVariable = implementation.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implementation.Name}");

            if (interfaceVariable != null)
            {
                services.AddScoped(interfaceVariable, implementation);
            }
            else
            {
                services.AddScoped(implementation); // nếu không có interface thì đăng ký chính class đó
            }
        }
        services.Configure<AuthSettings>(
            configuration.GetSection("AuthSettings").Bind
        );
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}