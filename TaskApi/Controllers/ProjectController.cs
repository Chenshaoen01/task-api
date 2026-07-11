using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Task.Application.Dtos.Project;

using Task.Application.ProjectHandlers.Queries.GetAllProjects;
using Task.Application.ProjectHandlers.Queries.GetProjectById;
using Task.Application.ProjectHandlers.Commands.CreateProject;
using Task.Application.ProjectHandlers.Commands.UpdateProject;
using Task.Application.ProjectHandlers.Commands.DeleteProject;

namespace TaskApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProjectController: ControllerBase
{
    private readonly GetAllProjectsHandler _getAllProjectsHandler;
    private readonly GetProjectByIdHandler _getProjectByIdHandler;
    private readonly CreateProjectHandler _createProjectHandler;
    private readonly UpdateProjectHandler _updateProjectHandler;
    private readonly DeleteProjectHandler _deleteProjectHandler;
    public ProjectController(
        GetAllProjectsHandler getAllProjectsHandler,
        GetProjectByIdHandler getProjectByIdHandler,
        CreateProjectHandler createProjectHandler,
        UpdateProjectHandler updateProjectHandler,
        DeleteProjectHandler deleteProjectHandler
    )
    {
        _getAllProjectsHandler = getAllProjectsHandler;
        _getProjectByIdHandler = getProjectByIdHandler;
        _createProjectHandler = createProjectHandler;
        _updateProjectHandler = updateProjectHandler;
        _deleteProjectHandler = deleteProjectHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectGet>>> GetAll()
    {
        return Ok(await _getAllProjectsHandler.Handle());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectGet?>> GetById(Guid id)
    {
        ProjectGet? getResult = await _getProjectByIdHandler.Handle(new GetProjectByIdQuery(id));
        if(getResult == null) return NotFound();
        return Ok(getResult);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectGet>> Create(ProjectCreate projectCreate)
    {
        return Ok(await _createProjectHandler.Handle(new CreateProjectCommand(projectCreate)));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectGet?>> Update(Guid id, ProjectUpdate projectUpdate)
    {
        ProjectGet? updateResult = await _updateProjectHandler.Handle(new UpdateProjectCommand(id, projectUpdate));
        if(updateResult == null) return NotFound();
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _deleteProjectHandler.Handle(new DeleteProjectCommand(id)) ? NoContent() : NotFound();
    }
}