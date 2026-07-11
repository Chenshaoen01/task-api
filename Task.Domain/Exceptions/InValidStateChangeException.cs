using Task.Domain.Entity;
namespace Task.Domain.Exceptions;

public class InValidStateChangeException: Exception
{
    public InValidStateChangeException(TaskState from, TaskState to) : base($"不允許的狀態轉換: {from} 轉換為 {to}") {}
}