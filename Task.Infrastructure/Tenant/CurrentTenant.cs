using Task.Application.Interface.TenantInterface;
using Microsoft.AspNetCore.Http;
using Task.Domain.Entity;

namespace Task.Infrastructure.InfrastructureTenant;

public class CurrentTenant: ICurrentTenant
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentTenant(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid CurrentTenantId
    {
        get
        {
            var claim = _accessor.HttpContext?.User.FindFirst("tenant_id");
            return Guid.TryParse(claim?.Value, out var id) ? id : Guid.Empty;
        }
    }

    public bool IsAdminUser
    {
        get
        {
            return _accessor.HttpContext?.User.IsInRole(nameof(UserRole.AdminUser)) ?? false;
        }
    }
}