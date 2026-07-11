using Task.Application.Dtos.Project;
namespace Task.Application.ProjectHandlers.Commands.UpdateProject;

public record UpdateProjectCommand(Guid id, ProjectUpdate projectUpdate);