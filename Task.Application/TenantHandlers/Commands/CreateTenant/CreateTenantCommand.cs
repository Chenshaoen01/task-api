using Task.Application.Dtos.Tenant;
namespace Task.Application.TenantHandlers.Commands.CreateTenant;

public record CreateTenantCommand(TenantCreate TenantCreate);