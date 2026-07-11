namespace Task.Domain.Exceptions;

public class InValidProjectIdException: Exception
{
    public InValidProjectIdException(): base("無效的專案代碼") {}
}