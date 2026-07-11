namespace Task.Application.Interface.TenantInterface;

public interface ICurrentTenant
{
    Guid CurrentTenantId { get; }

    bool IsAdminUser { get; }
}