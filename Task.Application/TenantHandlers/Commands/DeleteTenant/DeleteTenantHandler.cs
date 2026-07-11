using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;
using Task.Domain;

namespace Task.Application.TenantHandlers.Commands.DeleteTenant;

public class DeleteTenantHandler
{
    private ITaskDbContext _db;

    public DeleteTenantHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteTenantCommand command)
    {
        if(command.TenantId == SystemConstants.SystemTenantId) return false;
        Tenant? targetTenant = await _db.Tenants.FirstOrDefaultAsync(Tenant => Tenant.Id == command.TenantId);
        if(targetTenant == null) return false;

        List<Project> relatedProjects = _db.Projects.Where(project => project.TenantId == command.TenantId).ToList();
        foreach(Project project in relatedProjects)
        {
            List<TaskItem> relatedTasks = _db.Tasks.Where(task => task.ProjectId == project.Id).ToList();
            _db.Tasks.RemoveRange(relatedTasks);
            _db.Projects.Remove(project);
        }

        // 如果使用者的 TenantId 是 SystemTenantId，不刪除 
        List<User> relatedUsers = _db.Users
            .Where(user => user.TenantId != SystemConstants.SystemTenantId && user.TenantId == command.TenantId).ToList();
            _db.Users.RemoveRange(relatedUsers);

        await _db.Tasks.Where(task => task.TenantId == command.TenantId).ExecuteDeleteAsync();

        _db.Tenants.Remove(targetTenant);
        await _db.SaveChangesAsync();
        return true;
    }
}