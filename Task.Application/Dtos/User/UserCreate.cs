namespace Task.Application.Dtos.User;

public class UserCreate
{
    public required Guid TenantId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}