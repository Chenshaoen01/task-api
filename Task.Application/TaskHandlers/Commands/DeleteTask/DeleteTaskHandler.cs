using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TaskHandlers.Commands.DeleteTask;

public class DeleteTaskHandler
{
    private readonly ITaskDbContext _db;

    public DeleteTaskHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteTaskCommand command)
    {
        TaskItem? targetTaskItem = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == command.id);
        if(targetTaskItem == null) return false;
        
        _db.Tasks.Remove(targetTaskItem);
        await _db.SaveChangesAsync();
        return true;
    }
};