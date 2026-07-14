namespace Task.Application.Dtos.Task;

public class TaskItemCreate
{
    public Guid ProjectId { get; set; }
    public Guid AssigneeUserId { get; set; }
    public required string TaskTitle { get; set; }
    public string Description { get; set; } = "";
    public DateTime DueDate { get; set; }
};