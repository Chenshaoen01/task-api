using Task.Application.Interface.DataBase;
using Task.Application.Dtos.Task;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Task.Application.TaskHandlers.Queries.GetAllTasksOData;

public class GetAllTasksODataHandler
{
    private readonly ITaskDbContext _db;
    private readonly IMapper _mapper;

    public GetAllTasksODataHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IQueryable<TaskItemGet> Handle()
    {
        return _db.Tasks
        .ProjectTo<TaskItemGet>(_mapper.ConfigurationProvider);
    }
};