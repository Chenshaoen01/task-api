using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.Application.Interface.DataBase;
using Task.Application.Interface.PasswordHasher;
using Task.Infrastructure.PasswordHasher;
using Task.Application.Interface.Jwt;
using Task.Infrastructure.Jwt;
using Task.Infrastructure.DataBase;
using Task.Application.Interface.TenantInterface;
using Task.Infrastructure.InfrastructureTenant;

namespace Task.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<TaskDbContext>(options => 
          options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );

        services.AddScoped<ITaskDbContext>(sp => sp.GetRequiredService<TaskDbContext>());
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICurrentTenant, CurrentTenant>();
        return services;
    }
}