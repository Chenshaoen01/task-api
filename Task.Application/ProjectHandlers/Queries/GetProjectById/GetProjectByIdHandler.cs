using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Dtos.Project;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.ProjectHandlers.Queries.GetProjectById;

public class GetProjectByIdHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public GetProjectByIdHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ProjectGet?> Handle(GetProjectByIdQuery query)
    {
        Project? targetProject = await _db.Projects.FirstOrDefaultAsync(project => project.Id == query.projectId);
        if(targetProject == null) return null;
        return _mapper.Map<ProjectGet>(targetProject);
    }
}