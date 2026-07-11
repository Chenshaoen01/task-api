using Task.Application.Dtos.Task;
namespace Task.Application.TaskHandlers.Commands.CreateTask;

public record CreateTaskCommand(TaskItemCreate taskItemCreate);