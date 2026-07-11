using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Task.Application.Dtos.Task;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TaskHandlers.Queries.GetTaskById;

public class GetTaskByIdHandler
{
    private readonly ITaskDbContext _db;
    private readonly IMapper _mapper;

    public GetTaskByIdHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<TaskItemGet?> Handle(GetTaskByIdQuery query)
    {
        TaskItem? targetTaskItem = await _db.Tasks.FirstOrDefaultAsync(task => task.Id == query.id);
        if(targetTaskItem == null) return null;

        return _mapper.Map<TaskItemGet>(targetTaskItem);
    }
};