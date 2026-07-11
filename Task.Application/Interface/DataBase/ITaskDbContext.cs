using Microsoft.EntityFrameworkCore;
using Task.Domain.Entity;

namespace Task.Application.Interface.DataBase;

public interface ITaskDbContext
{
    public DbSet<Tenant> Tenants {get;}
    public DbSet<Project> Projects {get;}
    public DbSet<User> Users {get;}
    public DbSet<TaskItem> Tasks {get;}
    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
