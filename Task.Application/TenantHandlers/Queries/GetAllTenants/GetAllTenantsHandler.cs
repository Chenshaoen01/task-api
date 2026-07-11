using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;

namespace Task.Application.TenantHandlers.Queries.GetAllTenants;

public class GetAllTenantsHandler
{
    private ITaskDbContext _db;
    public GetAllTenantsHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Tenant>> Handle()
    {
        return await _db.Tenants.ToListAsync();
    }
}