using Task.Domain.Entity;
namespace Task.Application.Dtos.Task;

public class TaskItemGet
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid AssigneeUserId { get; set; }
    public string AssigneeUserName { get; set; } = "";
    public string TaskTitle { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public TaskState State { get; set; }
};