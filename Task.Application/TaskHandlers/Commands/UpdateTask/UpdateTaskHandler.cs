using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Task.Application.Dtos.Task;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TaskHandlers.Commands.UpdateTask;

public class UpdateTaskHandler
{
    private readonly ITaskDbContext _db;

    private readonly IMapper _mapper;

    public UpdateTaskHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<TaskItemGet?> Handle(UpdateTaskCommand command)
    {
        TaskItem? targetTaskItem = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == command.Id);
        if(targetTaskItem == null) return null;

        targetTaskItem.AssigneeUserId = command.taskItemUpdate.AssigneeUserId;
        targetTaskItem.TaskTitle = command.taskItemUpdate.TaskTitle;
        targetTaskItem.Description = command.taskItemUpdate.Description;
        targetTaskItem.DueDate = command.taskItemUpdate.DueDate;
        targetTaskItem.ChangeState(command.taskItemUpdate.State);

        await _db.SaveChangesAsync();
        return _mapper.Map<TaskItemGet>(targetTaskItem);
    }
};