using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Task.Application.Dtos.Project;
using Task.Application.Interface.DataBase;

namespace Task.Application.ProjectHandlers.Queries.GetAllProjects;

public class GetAllProjectsHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public GetAllProjectsHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectGet>> Handle()
    {
        return await _db.Projects
          .ProjectTo<ProjectGet>(_mapper.ConfigurationProvider)
          .ToListAsync();
    }
}