using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.ProjectHandlers.Commands.DeleteProject;

public class DeleteProjectHandler
{
    private ITaskDbContext _db;

    public DeleteProjectHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteProjectCommand command)
    {
        Project? targetProject = await _db.Projects.FirstOrDefaultAsync(project => project.Id == command.projectId);

        if(targetProject == null) return false;

        await _db.Tasks.Where(task => task.ProjectId == command.projectId).ExecuteDeleteAsync();

        _db.Projects.Remove(targetProject);
        await _db.SaveChangesAsync();
        return true;
    }
}