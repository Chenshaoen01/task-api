using Task.Domain.Entity;
namespace Task.Application.Dtos.Task;

public class TaskItemGet
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string TaskTitle { get; set; } = "";
    public DateTime DueDate { get; set; }
    public TaskState State { get; set; }
};