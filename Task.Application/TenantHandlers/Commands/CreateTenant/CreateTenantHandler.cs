using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;

namespace Task.Application.TenantHandlers.Commands.CreateTenant;

public class CreateTenantHandler
{
    private ITaskDbContext _db;
    public CreateTenantHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<Tenant?> Handle(CreateTenantCommand command)
    {
        Tenant newTenant = new Tenant()
        {
            Id = Guid.NewGuid(),
            Name = command.TenantCreate.Name
        };
        _db.Tenants.Add(newTenant);
        await _db.SaveChangesAsync();

        return newTenant;
    }
}