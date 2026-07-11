using Task.Application.Dtos.Tenant;
namespace Task.Application.TenantHandlers.Commands.UpdateTenant;

public record UpdateTenantCommand(Guid id, TenantUpdate TenantUpdate);