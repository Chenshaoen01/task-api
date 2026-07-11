using Task.Domain.Entity;
using AutoMapper;
using Task.Application.Dtos.Tenant;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TenantHandlers.Commands.UpdateTenant;

public class UpdateTenantHandler
{
    private ITaskDbContext _db;
    public UpdateTenantHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<Tenant?> Handle(UpdateTenantCommand command)
    {
        Tenant? targetTenant = await _db.Tenants.FirstOrDefaultAsync(Tenant => Tenant.Id == command.id);
        if(targetTenant == null) return null;

        targetTenant.Name = command.TenantUpdate.Name;
        await _db.SaveChangesAsync();

        return targetTenant;
    }
}