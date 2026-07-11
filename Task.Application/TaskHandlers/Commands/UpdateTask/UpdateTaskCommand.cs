using Task.Application.Dtos.Task;

namespace Task.Application.TaskHandlers.Commands.UpdateTask;

public record UpdateTaskCommand(Guid Id, TaskItemUpdate taskItemUpdate);