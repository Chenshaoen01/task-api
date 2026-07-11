using Task.Application.Interface.DataBase;
using Task.Application.Dtos.Task;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.TaskHandlers.Queries.GetAllTasks;

public class GetAllTasksHandler
{
    private readonly ITaskDbContext _db;
    private readonly IMapper _mapper;

    public GetAllTasksHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItemGet>> Handle(GetAllTasksQuery query)
    {
        return await _db.Tasks
        .Where(task => task.ProjectId == query.projectId)
        .ProjectTo<TaskItemGet>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }
};