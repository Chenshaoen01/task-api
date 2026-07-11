namespace Task.Application.Dtos.User;

public class UserGet
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}