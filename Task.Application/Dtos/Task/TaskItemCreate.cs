namespace Task.Application.Dtos.Task;

public class TaskItemCreate
{
    public Guid ProjectId { get; set; }
    public required string TaskTitle { get; set; }
    public DateTime DueDate { get; set; }
};