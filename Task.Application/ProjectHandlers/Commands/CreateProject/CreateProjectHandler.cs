using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Dtos.Project;
using Task.Application.Interface.DataBase;

namespace Task.Application.ProjectHandlers.Commands.CreateProject;

public class CreateProjectHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public CreateProjectHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ProjectGet?> Handle(CreateProjectCommand command)
    {
        Project newProject = new Project()
        {
            Id = Guid.NewGuid(),
            Name = command.projectCreate.Name,
            Description = command.projectCreate.Description
        };
        _db.Projects.Add(newProject);
        await _db.SaveChangesAsync();

        return _mapper.Map<ProjectGet>(newProject);
    }
}