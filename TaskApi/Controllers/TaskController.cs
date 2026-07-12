using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Task.Domain.Exceptions;
using Task.Application.Dtos.Task;

using Task.Application.TaskHandlers.Queries.GetAllTasks;
using Task.Application.TaskHandlers.Queries.GetAllTasksOData;
using Task.Application.TaskHandlers.Queries.GetTaskById;
using Task.Application.TaskHandlers.Commands.CreateTask;
using Task.Application.TaskHandlers.Commands.UpdateTask;
using Task.Application.TaskHandlers.Commands.DeleteTask;

namespace TaskApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[ODataAttributeRouting]
public class TaskController : ControllerBase
{
    private readonly GetAllTasksHandler _getAllTasksHandler;
    private readonly GetAllTasksODataHandler _getAllTasksODataHandler;
    private readonly GetTaskByIdHandler _getTaskByIdHandler;
    private readonly CreateTaskHandler _createTaskHandler;
    private readonly UpdateTaskHandler _updateTaskHandler;
    private readonly DeleteTaskHandler _deleteTaskHandler;

    public TaskController(
        GetAllTasksHandler getAllTasksHandler,
        GetAllTasksODataHandler getAllTasksODataHandler,
        GetTaskByIdHandler getTaskByIdHandler,
        CreateTaskHandler createTaskHandler,
        UpdateTaskHandler updateTaskHandler,
        DeleteTaskHandler deleteTaskHandler
    )
    {
        _getAllTasksHandler = getAllTasksHandler;
        _getAllTasksODataHandler = getAllTasksODataHandler;
        _getTaskByIdHandler = getTaskByIdHandler;
        _createTaskHandler = createTaskHandler;
        _updateTaskHandler = updateTaskHandler;
        _deleteTaskHandler = deleteTaskHandler;
    }

    [HttpGet("odata/Tasks")]
    [EnableQuery]
    public ActionResult<IEnumerable<TaskItemGet>> ODataGetAll()
    {
        return Ok(_getAllTasksODataHandler.Handle());
    }

    [HttpGet("getProjectTasks/{projectId}")]
    public async Task<ActionResult<IEnumerable<TaskItemGet>>> GetAll(Guid projectId)
    {
        return Ok(await _getAllTasksHandler.Handle(new GetAllTasksQuery(projectId)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemGet?>> GetById(Guid id)
    {
        TaskItemGet? taskItem = await _getTaskByIdHandler.Handle(new GetTaskByIdQuery(id));
        return taskItem == null ? NotFound() : Ok(taskItem);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemGet>> Create(TaskItemCreate taskItemCreate)
    {
        try
        {            
          TaskItemGet? created = await _createTaskHandler.Handle(new CreateTaskCommand(taskItemCreate));
          return Ok(created);
        } catch(InValidProjectIdException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItemGet?>> Update(Guid id, TaskItemUpdate taskItemUpdate)
    {
        try
        {            
          TaskItemGet? taskItem = await _updateTaskHandler.Handle(new UpdateTaskCommand(id, taskItemUpdate));
          return taskItem == null? NotFound() : taskItem;
        } catch(InValidStateChangeException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _deleteTaskHandler.Handle(new DeleteTaskCommand(id)) ? NoContent() : NotFound();
    }
}
