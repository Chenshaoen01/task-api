using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TenantHandlers.Queries.GetTenantById;

public class GetTenantByIdHandler
{
    private ITaskDbContext _db;
    public GetTenantByIdHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<Tenant?> Handle(GetTenantByIdQuery query)
    {
        Tenant? targetTenant = await _db.Tenants.FirstOrDefaultAsync(Tenant => Tenant.Id == query.TenantId);
        return targetTenant != null ? targetTenant : null;
    }
}