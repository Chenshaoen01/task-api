using Task.Domain.Entity;
using AutoMapper;
using Task.Application.Dtos.Project;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.ProjectHandlers.Commands.UpdateProject;

public class UpdateProjectHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public UpdateProjectHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ProjectGet?> Handle(UpdateProjectCommand command)
    {
        Project? targetProject = await _db.Projects.FirstOrDefaultAsync(project => project.Id == command.id);
        if(targetProject == null) return null;

        targetProject.Name = command.projectUpdate.Name;
        await _db.SaveChangesAsync();

        return _mapper.Map<ProjectGet>(targetProject);
    }
}