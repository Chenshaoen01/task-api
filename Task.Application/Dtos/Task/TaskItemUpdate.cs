using Task.Domain.Entity;
namespace Task.Application.Dtos.Task;

public class TaskItemUpdate
{
    public Guid AssigneeUserId { get; set; }
    public required string TaskTitle { get; set; }
    public string Description { get; set; } = "";
    public DateTime DueDate { get; set; }
    public TaskState State { get; set; }
};