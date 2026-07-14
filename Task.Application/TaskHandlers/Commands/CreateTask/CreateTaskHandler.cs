using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Task.Application.Dtos.Task;
using Task.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TaskHandlers.Commands.CreateTask;

public class CreateTaskHandler
{
    private readonly ITaskDbContext _db;
    private readonly IMapper _mapper;

    public CreateTaskHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<TaskItemGet?> Handle(CreateTaskCommand command)
    {
        Project? targetProject = await _db.Projects.FirstOrDefaultAsync(project => project.Id == command.taskItemCreate.ProjectId);
        if(targetProject == null)
        {
            throw new InValidProjectIdException();
        }
        ;
        TaskItem newTaskItem = new TaskItem
        {
            Id = Guid.NewGuid(),
            ProjectId = command.taskItemCreate.ProjectId,
            AssigneeUserId = command.taskItemCreate.AssigneeUserId,
            TaskTitle = command.taskItemCreate.TaskTitle,
            Description = command.taskItemCreate.Description,
            CreatedAt = DateTime.UtcNow,
            DueDate = command.taskItemCreate.DueDate,
            State = TaskState.Todo
        };

        _db.Tasks.Add(newTaskItem);
        await _db.SaveChangesAsync();
        return _mapper.Map<TaskItemGet>(newTaskItem);
    }
};