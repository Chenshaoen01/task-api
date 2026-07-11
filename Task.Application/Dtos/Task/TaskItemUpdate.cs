using Task.Domain.Entity;
namespace Task.Application.Dtos.Task;

public class TaskItemUpdate
{
    public required string TaskTitle { get; set; }
    public DateTime DueDate { get; set; }
    public TaskState State { get; set; }
};