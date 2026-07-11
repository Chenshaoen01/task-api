using Task.Domain.Exceptions;
namespace Task.Domain.Entity;

public enum TaskState { Todo, Inprogress, Done }

public class TaskItem
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid ProjectId { get; set; }
    public string TaskTitle { get; set; } = "";
    public DateTime DueDate { get; set; }
    public TaskState State { get; set; }
    public void ChangeState(TaskState newState)
    {
        if(newState == this.State) return; 
        bool isChangeValid = false;
        if(this.State == TaskState.Todo) isChangeValid = (newState == TaskState.Inprogress);
        if(this.State == TaskState.Inprogress) isChangeValid = (newState == TaskState.Done);
        
        if(!isChangeValid)
        {
            throw new InValidStateChangeException(this.State, newState);
        } 
        
        this.State = newState;
    }
}