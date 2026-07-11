using Task.Application.Dtos.Project;
namespace Task.Application.ProjectHandlers.Commands.CreateProject;

public record CreateProjectCommand(ProjectCreate projectCreate);